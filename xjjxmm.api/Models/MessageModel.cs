﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xjjxmm.Common;

namespace xjjxmm.Models
{
    public class MessageModel<T>
    {    
        /// <summary>
         /// 状态码
         /// </summary>
        public int status { get; set; } = 200;
        /// <summary>
        /// 操作是否成功
        /// </summary>
        public bool success { get; set; } = true;
        /// <summary>
        /// 返回信息
        /// </summary>
        public string msg { get; set; } = CommonConstant.Message_Successfully;
        /// <summary>
        /// 返回数据集合
        /// </summary>
        public T response { get; set; }
    }

    
}
