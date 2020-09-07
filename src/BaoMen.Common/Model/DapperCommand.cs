using Dapper;
using System.Data;
using System.Threading;

namespace BaoMen.Common.Model
{
    /// <summary>
    /// Dapper数据库命令
    /// </summary>
    public class DapperCommand
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DapperCommand()
        {
            Flags = CommandFlags.Buffered;
            //CancellationToken = default(CancellationToken);
        }

        /// <summary>
        /// The command (sql or a stored-procedure name) to execute
        /// </summary>
        public string CommandText { get; set; }

        /// <summary>
        /// The parameters associated with the command
        /// </summary>
        public object Parameters { get; set; }

        /// <summary>
        /// The active transaction for the command
        /// </summary>
        public IDbTransaction Transaction { get; set; }

        /// <summary>
        /// The effective timeout for the command
        /// </summary>
        public int? CommandTimeout { get; set; }

        /// <summary>
        /// The type of command that the command-text represents
        /// </summary>
        public CommandType? CommandType { get; set; }

        /// <summary>
        /// Additional state flags against this command
        /// </summary>
        public CommandFlags Flags { get; set; }

        /// <summary>
        /// For asynchronous operations, the cancellation-token
        /// </summary>
        public CancellationToken CancellationToken { get; set; }

        //public int StartRowIndex { get; set; }

        //public int MaximumRows { get; set; }
    }
}
