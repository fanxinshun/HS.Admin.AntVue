using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.MiniPrograms
{
    /// <summary>
    /// 页面
    /// </summary>
    [Table("app_page")]
    public class app_page
    {

        /// <summary>
        /// id
        /// </summary>
        [Key, Column(Order = 1)]
        public Int32 id { get; set; }

        /// <summary>
        /// page_name
        /// </summary>
        public String page_name { get; set; }

        /// <summary>
        /// 1为首页,2为活动页,3为频道页
        /// </summary>
        public Int32? page_type { get; set; }

        /// <summary>
        /// 0为Pad端， 1为手机端
        /// </summary>
        public Int32 is_phone { get; set; }

        /// <summary>
        /// created_time
        /// </summary>
        public DateTime created_time { get; set; }

        /// <summary>
        /// updated_time
        /// </summary>
        public DateTime? updated_time { get; set; }

        /// <summary>
        /// deleted_time
        /// </summary>
        public DateTime? deleted_time { get; set; }

        /// <summary>
        /// is_deleted
        /// </summary>
        public Int32? is_deleted { get; set; }

        /// <summary>
        /// 0为页面 ，1为页签面
        /// </summary>
        public Int32? is_tab { get; set; }

        /// <summary>
        /// 页签页面Id
        /// </summary>
        public Int32? page_id { get; set; }

        /// <summary>
        /// 页面说明
        /// </summary>
        public String page_remark { get; set; }

        /// <summary>
        /// 显示方向(0:竖屏,1:横屏)
        /// </summary>
        public Int32? screen_type { get; set; }

        /// <summary>
        /// 排序编号
        /// </summary>
        public Int32? order_id { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public Int32? is_show { get; set; }

        /// <summary>
        /// 背景图
        /// </summary>
        public String bg_pic { get; set; }

    }
}