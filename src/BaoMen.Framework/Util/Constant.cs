using System;
using System.Collections.Generic;
using System.Text;

namespace BaoMen.Framework.Util
{
    /// <summary>
    /// 常量
    /// </summary>
    public class Constant
    {
        /// <summary>
        /// 采购单
        /// </summary>
        public class PurchaseOrder
        {
            /// <summary>
            /// 工作流编码
            /// </summary>
            public const string WorkflowCode = "PurchaseOrder";

            /// <summary>
            /// 工作流活动编码
            /// </summary>
            public class ActivityCode
            {
                /// <summary>
                /// 保存
                /// </summary>
                public const string Create = "Create";
                /// <summary>
                /// 提交
                /// </summary>
                public const string CreateAndSubmit = "CreateAndSubmit";
                /// <summary>
                /// 编辑
                /// </summary>
                public const string Edit = "Edit";

                /// <summary>
                /// 提交审核
                /// </summary>
                public const string Submit = "Submit";

                /// <summary>
                /// 取消采购
                /// </summary>
                public const string Cancel = "Cancel";

                /// <summary>
                /// 审批通过
                /// </summary>
                public const string Approve = "Approve";

                /// <summary>
                /// 退回修改
                /// </summary>
                public const string SendBack = "SendBack";

                /// <summary>
                /// 付款
                /// </summary>
                public const string Pay = "Pay";

                /// <summary>
                /// 确认收货
                /// </summary>
                public const string Receiving = "Receiving";
                /// <summary>
                /// 收货完成
                /// </summary>
                public const string Received = "Received";
                /// <summary>
                /// 付款完成
                /// </summary>
                public const string Paid = "Paid";



            }

            /// <summary>
            /// 付款状态
            /// </summary>
            public class PayState
            {
                /// <summary>
                /// 未支付
                /// </summary>
                public const int NotPay = 1;

                /// <summary>
                /// 部分支付
                /// </summary>
                public const int PartialPay = 2;

                /// <summary>
                /// 已付清
                /// </summary>
                public const int Paid = 3;
            }
        }

        /// <summary>
        /// 库存单
        /// </summary>
        public class StorageOrder
        {
            /// <summary>
            /// 工作流编码
            /// </summary>
            public const string WorkflowCode = "StorageOrder";

            /// <summary>
            /// 工作流活动编码
            /// </summary>
            public class ActivityCode
            {
                /// <summary>
                /// 提交审核
                /// </summary>
                public const string Submit = "Submit";

                /// <summary>
                /// 取消采购
                /// </summary>
                public const string Cancel = "Cancel";

                /// <summary>
                /// 审批通过
                /// </summary>
                public const string Approve = "Approve";

                /// <summary>
                /// 新建
                /// </summary>
                public const string Create = "Create";
            }

            /// <summary>
            /// 工作流动作编码
            /// </summary>
            public class ActionCode
            {
                /// <summary>
                /// 保存（新建）
                /// </summary>
                public const string Create = "Create";
            }
        }

        /// <summary>
        /// 付款单
        /// </summary>
        public class PaymentOrder
        {
            /// <summary>
            /// 工作流编码
            /// </summary>
            public const string WorkflowCode = "PaymentOrder";

            /// <summary>
            /// 工作流活动编码
            /// </summary>
            public class ActivityCode
            {
                /// <summary>
                /// 保存
                /// </summary>
                public const string Create = "Create";

                /// <summary>
                /// 提交
                /// </summary>
                public const string Submit = "Submit";

                /// <summary>
                /// 取消付款
                /// </summary>
                public const string Cancel = "Cancel";

                /// <summary>
                /// 编辑
                /// </summary>
                public const string Edit = "Edit";

                /// <summary>
                /// 同意
                /// </summary>
                public const string Approve = "Approve";

                /// <summary>
                /// 返回修改
                /// </summary>
                public const string SendBack = "SendBack";
            }

            /// <summary>
            /// 工作流动作编码
            /// </summary>
            public partial class ActionCode
            {
                /// <summary>
                /// 保存
                /// </summary>
                public const string Create = "Create";

                /// <summary>
                /// 保存并提交
                /// </summary>
                public const string CreateAndSubmit = "CreateAndSubmit";

                /// <summary>
                /// 编辑
                /// </summary>
                public const string Edit = "Edit";

                /// <summary>
                /// 编辑并提交
                /// </summary>
                public const string EditAndSubmit = "EditAndSubmit";
            }
        }

        /// <summary>
        /// 付款单
        /// </summary>
        public class PersonPaymentOrder
        {
            /// <summary>
            /// 工作流编码
            /// </summary>
            public const string WorkflowCode = "PersonPaymentOrder";

            /// <summary>
            /// 工作流活动编码
            /// </summary>
            public class ActivityCode
            {
                /// <summary>
                /// 保存
                /// </summary>
                public const string Create = "Create";

                /// <summary>
                /// 提交
                /// </summary>
                public const string Submit = "Submit";

                /// <summary>
                /// 取消付款
                /// </summary>
                public const string Cancel = "Cancel";

                /// <summary>
                /// 编辑
                /// </summary>
                public const string Edit = "Edit";

                /// <summary>
                /// 同意
                /// </summary>
                public const string Approve = "Approve";

                /// <summary>
                /// 返回修改
                /// </summary>
                public const string SendBack = "SendBack";
            }

            /// <summary>
            /// 工作流动作编码
            /// </summary>
            public partial class ActionCode
            {
                /// <summary>
                /// 保存
                /// </summary>
                public const string Create = "Create";

                /// <summary>
                /// 保存并提交
                /// </summary>
                public const string CreateAndSubmit = "CreateAndSubmit";

                /// <summary>
                /// 编辑
                /// </summary>
                public const string Edit = "Edit";

                /// <summary>
                /// 编辑并提交
                /// </summary>
                public const string EditAndSubmit = "EditAndSubmit";
            }
        }

        /// <summary>
        /// 收款单
        /// </summary>
        public class CollectionOrder
        {
            /// <summary>
            /// 工作流编码
            /// </summary>
            public const string WorkflowCode = "CollectionOrder";

            /// <summary>
            /// 工作流活动编码
            /// </summary>
            public class ActivityCode
            {
                /// <summary>
                /// 保存
                /// </summary>
                public const string Create = "Create";

                /// <summary>
                /// 提交
                /// </summary>
                public const string Submit = "Submit";

                /// <summary>
                /// 取消付款
                /// </summary>
                public const string Cancel = "Cancel";

                /// <summary>
                /// 编辑
                /// </summary>
                public const string Edit = "Edit";

                /// <summary>
                /// 同意
                /// </summary>
                public const string Approve = "Approve";

                /// <summary>
                /// 返回修改
                /// </summary>
                public const string SendBack = "SendBack";
            }

            /// <summary>
            /// 工作流动作编码
            /// </summary>
            public partial class ActionCode
            {
                /// <summary>
                /// 保存
                /// </summary>
                public const string Create = "Create";

                /// <summary>
                /// 保存并提交
                /// </summary>
                public const string CreateAndSubmit = "CreateAndSubmit";

                /// <summary>
                /// 编辑
                /// </summary>
                public const string Edit = "Edit";

                /// <summary>
                /// 编辑并提交
                /// </summary>
                public const string EditAndSubmit = "EditAndSubmit";
            }
        }

        /// <summary>
        /// 现货进货单
        /// </summary>
        public class SpotOrder
        {
            /// <summary>
            /// 工作流编码
            /// </summary>
            public const string WorkflowCode = "SpotOrder";

            /// <summary>
            /// 工作流活动编码
            /// </summary>
            public class ActivityCode
            {
                /// <summary>
                /// 新建
                /// </summary>
                public const string Create = "Create";

                /// <summary>
                /// 编辑
                /// </summary>
                public const string Edit = "Edit";

                /// <summary>
                /// 提交审核
                /// </summary>
                public const string Submit = "Submit";

                /// <summary>
                /// 取消采购
                /// </summary>
                public const string Cancel = "Cancel";

                /// <summary>
                /// 审批通过
                /// </summary>
                public const string Approve = "Approve";

                /// <summary>
                /// 退回修改
                /// </summary>
                public const string SendBack = "SendBack";

                /// <summary>
                /// 付款
                /// </summary>
                public const string Pay = "Pay";

                /// <summary>
                /// 付款完成
                /// </summary>
                public const string Paid = "Paid";

                /// <summary>
                /// 收货
                /// </summary>
                public const string Receiving = "Receiving";

                /// <summary>
                /// 自动收货
                /// </summary>
                public const string AutoReceiving = "AutoReceiving";

                /// <summary>
                /// 收货完成
                /// </summary>
                public const string Received = "Received";

            }

            /// <summary>
            /// 工作流动作编码
            /// </summary>
            public class ActionCode
            {
                /// <summary>
                /// 新建
                /// </summary>
                public const string Create = "Create";

                /// <summary>
                /// 新建并提交
                /// </summary>
                public const string CreateAndSubmit = "CreateAndSubmit";

                /// <summary>
                /// 编辑
                /// </summary>
                public const string Edit = "Edit";

                /// <summary>
                /// 编辑并提交
                /// </summary>
                public const string EditAndSubmit = "EditAndSubmit";
            }

            /// <summary>
            /// 付款状态
            /// </summary>
            public class PayState
            {
                /// <summary>
                /// 未支付
                /// </summary>
                public const int NotPay = 1;

                /// <summary>
                /// 部分支付
                /// </summary>
                public const int PartialPay = 2;

                /// <summary>
                /// 已付清
                /// </summary>
                public const int Paid = 3;
            }
        }

        /// <summary>
        /// 现货销售单
        /// </summary>
        public class SpotSalesOrder
        {
            /// <summary>
            /// 工作流编码
            /// </summary>
            public const string WorkflowCode = "SpotSalesOrder";

            /// <summary>
            /// 工作流活动编码
            /// </summary>
            public class ActivityCode
            {
                /// <summary>
                /// 保存
                /// </summary>
                public const string Create = "Create";

                /// <summary>
                /// 编辑
                /// </summary>
                public const string Edit = "Edit";

                /// <summary>
                /// 提交审核
                /// </summary>
                public const string Submit = "Submit";

                /// <summary>
                /// 取消销售
                /// </summary>
                public const string Cancel = "Cancel";

                /// <summary>
                /// 审批通过
                /// </summary>
                public const string Approve = "Approve";

                /// <summary>
                /// 退回修改
                /// </summary>
                public const string SendBack = "SendBack";

                /// <summary>
                /// 收款
                /// </summary>
                public const string Collect = "Collect";

                /// <summary>
                /// 收款完成
                /// </summary>
                public const string Collected = "Collected";

                /// <summary>
                /// 发货
                /// </summary>
                public const string Delivering = "Delivering";

                /// <summary>
                /// 自动发货
                /// </summary>
                public const string AutoDelivering = "AutoDelivering";

                /// <summary>
                /// 发货完成
                /// </summary>
                public const string Delivered = "Delivered";

            }

            /// <summary>
            /// 工作流动作编码
            /// </summary>
            public class ActionCode
            {
                /// <summary>
                /// 新建
                /// </summary>
                public const string Create = "Create";

                /// <summary>
                /// 新建并提交
                /// </summary>
                public const string CreateAndSubmit = "CreateAndSubmit";

                /// <summary>
                /// 编辑
                /// </summary>
                public const string Edit = "Edit";

                /// <summary>
                /// 编辑并提交
                /// </summary>
                public const string EditAndSubmit = "EditAndSubmit";
            }

            /// <summary>
            /// 付款状态
            /// </summary>
            public class PayState
            {
                /// <summary>
                /// 未支付
                /// </summary>
                public const int NotPay = 1;

                /// <summary>
                /// 部分支付
                /// </summary>
                public const int PartialPay = 2;

                /// <summary>
                /// 已付清
                /// </summary>
                public const int Paid = 3;
            }
        }
    }
}
