﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JWT
{
    /// <summary>
    /// Jwt Validator.
    /// </summary>
    public sealed class JwtValidator : IJwtValidator
    {
        /// <summary>
        /// Describes instants in time, defined as the number of seconds that have elapsed since 00:00:00 UTC, Thursday, 1 January 1970, not counting leap seconds.
        /// See https://en.wikipedia.org/wiki/Unix_time />
        /// </summary>
        public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        private readonly IJsonSerializer _jsonSerializer;
        private readonly IDateTimeProvider _dateTimeProvider;

        /// <summary>
        /// Creates an instance of <see cref="JwtValidator" />.
        /// </summary>
        /// <param name="jsonSerializer">The Json Serializer.</param>
        /// <param name="dateTimeProvider">The DateTime Provider.</param>
        public JwtValidator(IJsonSerializer jsonSerializer, IDateTimeProvider dateTimeProvider)
        {
            _jsonSerializer = jsonSerializer;
            _dateTimeProvider = dateTimeProvider;
        }

        /// <inheritdoc />
        public void Validate(string payloadJson, string decodedCrypto, string decodedSignature)
        {
            if (!CompareCryptoWithSignature(decodedCrypto, decodedSignature))
            {
                throw new SignatureVerificationException("Invalid signature")
                {
                    Expected = decodedCrypto,
                    Received = decodedSignature
                };
            }

            var payloadData = _jsonSerializer.Deserialize<Dictionary<string, object>>(payloadJson);

            var now = _dateTimeProvider.GetNow();
          

            // verify exp claim https://tools.ietf.org/html/draft-ietf-oauth-json-web-token-32#section-4.1.4
            object expObj;
            if (payloadData.TryGetValue("exp", out expObj))
            {
                if (expObj == null)
                {
                    throw new SignatureVerificationException("Claim 'exp' must be a number.");
                }
                var expired = Convert.ToString(expObj).getJWTDatetime();



                if (now >= expired)
                {
                    throw new TokenExpiredException("Token has expired.")
                    {
                        Expiration = now,
                        PayloadData = payloadData
                    };
                }
            }

            // verify nbf claim https://tools.ietf.org/html/draft-ietf-oauth-json-web-token-32#section-4.1.5
            object nbfObj;
            if (payloadData.TryGetValue("nbf", out nbfObj))
            {
                if (nbfObj == null)
                {
                    throw new SignatureVerificationException("Claim 'nbf' must be a number.");
                }

                double nbfValue;
                try
                {
                    nbfValue = Convert.ToDouble(nbfObj);
                }
                catch
                {
                    throw new SignatureVerificationException("Claim 'nbf' must be a number.");
                }
                /*
                if (secondsSinceEpoch < nbfValue)
                {
                    throw new SignatureVerificationException("Token is not yet valid.");
                }*/
            }
        }

        /// <remarks>In the future this method can be open for extension so made protected virtual</remarks>
        private static bool CompareCryptoWithSignature(string decodedCrypto, string decodedSignature)
        {
            if (decodedCrypto.Length != decodedSignature.Length)
            {
                return false;
            }

            byte[] decodedCryptoBytes = Encoding.ASCII.GetBytes(decodedCrypto);
            byte[] decodedSignatureBytes = Encoding.ASCII.GetBytes(decodedSignature);

            byte result = 0;
            for (int i = 0; i < decodedCrypto.Length; i++)
            {
                result |= (byte)(decodedCryptoBytes[i] ^ decodedSignatureBytes[i]);
            }

            return result == 0;
        }
    }
}