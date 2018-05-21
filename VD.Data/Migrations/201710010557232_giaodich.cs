namespace VD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class giaodich : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ViTien", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "IsVip1", c => c.Boolean(nullable: false));
            AddColumn("dbo.Settings", "menh_gia_the_cao_mua_thanh_vien_vip1", c => c.Int(nullable: false));
            DropColumn("dbo.Settings", "max_num_top_job");
            DropColumn("dbo.Settings", "max_korean_candidates_hompage");
            DropColumn("dbo.Settings", "etmp_reply_employer_register_content");
            DropColumn("dbo.Settings", "etmp_reply_employer_register_title");
            DropColumn("dbo.Settings", "etmp_reply_employer_register_content_send_admin");
            DropColumn("dbo.Settings", "etmp_reply_employer_register_title_send_admin");
            DropColumn("dbo.Settings", "etmp_reply_cadidate_register_content");
            DropColumn("dbo.Settings", "etmp_reply_cadidate_register_title");
            DropColumn("dbo.Settings", "etmp_reply_cadidate_register_content_send_admin");
            DropColumn("dbo.Settings", "etmp_reply_cadidate_register_title_send_admin");
            DropColumn("dbo.Settings", "etmp_reply_purchase_wallet_content");
            DropColumn("dbo.Settings", "etmp_reply_purchase_wallet_title");
            DropColumn("dbo.Settings", "etmp_statis_job_cs_title");
            DropColumn("dbo.Settings", "etmp_statis_job_cs_content");
            DropColumn("dbo.Settings", "fee_score_post_recruitment");
            DropColumn("dbo.Settings", "fee_score_filter_profile");
            DropColumn("dbo.Settings", "fee_score_label_urgent");
            DropColumn("dbo.Settings", "fee_score_label_hot");
            DropColumn("dbo.Settings", "fee_score_label_new");
            DropColumn("dbo.Settings", "fee_score_label_top");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Settings", "fee_score_label_top", c => c.Int(nullable: false));
            AddColumn("dbo.Settings", "fee_score_label_new", c => c.Int(nullable: false));
            AddColumn("dbo.Settings", "fee_score_label_hot", c => c.Int(nullable: false));
            AddColumn("dbo.Settings", "fee_score_label_urgent", c => c.Int(nullable: false));
            AddColumn("dbo.Settings", "fee_score_filter_profile", c => c.Int(nullable: false));
            AddColumn("dbo.Settings", "fee_score_post_recruitment", c => c.Int(nullable: false));
            AddColumn("dbo.Settings", "etmp_statis_job_cs_content", c => c.String());
            AddColumn("dbo.Settings", "etmp_statis_job_cs_title", c => c.String());
            AddColumn("dbo.Settings", "etmp_reply_purchase_wallet_title", c => c.String());
            AddColumn("dbo.Settings", "etmp_reply_purchase_wallet_content", c => c.String());
            AddColumn("dbo.Settings", "etmp_reply_cadidate_register_title_send_admin", c => c.String());
            AddColumn("dbo.Settings", "etmp_reply_cadidate_register_content_send_admin", c => c.String());
            AddColumn("dbo.Settings", "etmp_reply_cadidate_register_title", c => c.String());
            AddColumn("dbo.Settings", "etmp_reply_cadidate_register_content", c => c.String());
            AddColumn("dbo.Settings", "etmp_reply_employer_register_title_send_admin", c => c.String());
            AddColumn("dbo.Settings", "etmp_reply_employer_register_content_send_admin", c => c.String());
            AddColumn("dbo.Settings", "etmp_reply_employer_register_title", c => c.String());
            AddColumn("dbo.Settings", "etmp_reply_employer_register_content", c => c.String());
            AddColumn("dbo.Settings", "max_korean_candidates_hompage", c => c.Int(nullable: false));
            AddColumn("dbo.Settings", "max_num_top_job", c => c.Int(nullable: false));
            DropColumn("dbo.Settings", "menh_gia_the_cao_mua_thanh_vien_vip1");
            DropColumn("dbo.Users", "IsVip1");
            DropColumn("dbo.Users", "ViTien");
        }
    }
}
