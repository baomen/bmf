using System;
using System.Text;
using System.Text.RegularExpressions;

namespace BaoMen.Common.Model
{
    /// <summary>
    /// QuerySql
    /// </summary>
    public class QuerySql
    {
        /// <summary>
        /// Construct new QuerySql instance.
        /// </summary>
        public QuerySql()
        {
        }

        /// <summary>
        /// Construct new QuerySql instance.
        /// </summary>
        /// <param name="sql">query string.Important:IdFiled must be the first column.</param>
        public QuerySql(string sql)
        {
            this.sqlString = sql.Trim();
            sql = this.sqlString.ToLower();
            if (!sql.StartsWith("select "))
            {
                throw new ArgumentException("query string must start with \"select\" keyword");
            }
            int fromPosition = GetFromPosition(sql);
            if (fromPosition == -1)
            {
                throw new ArgumentException("query string not include \"from\" keyword");
            }
            int wherePosition = GetWherePosition(sql);
            int groupPosition = sql.LastIndexOf(" group by ");
            int havingPosition = sql.LastIndexOf(" having ");
            int orderPosition = sql.LastIndexOf(" order by ");
            this.column = this.sqlString.Substring(7, fromPosition - 7);
            if (string.IsNullOrEmpty(this.column))
            {
                throw new ArgumentException("query string not include any column");
            }
            //this.idField = GetDefaultIdField();
            if (wherePosition > -1)
            {
                this.from = this.sqlString.Substring(fromPosition + 6, wherePosition - fromPosition - 6);
                if (groupPosition > -1)
                {
                    this.where = this.sqlString.Substring(wherePosition + 7, groupPosition - wherePosition - 7);
                    if (havingPosition > -1)
                    {
                        this.group = this.sqlString.Substring(groupPosition + 10, havingPosition - groupPosition - 10);
                        if (orderPosition > -1)
                        {
                            this.having = this.sqlString.Substring(havingPosition + 8, orderPosition - havingPosition - 8);
                            this.order = this.sqlString.Substring(orderPosition + 10);
                        }
                        else
                        {
                            this.having = this.sqlString.Substring(havingPosition + 8);
                        }
                    }
                    else if (orderPosition > -1)
                    {
                        this.group = this.sqlString.Substring(groupPosition + 10, orderPosition - groupPosition - 10);
                        this.order = this.sqlString.Substring(orderPosition + 10);
                    }
                    else
                    {
                        this.group = this.sqlString.Substring(groupPosition + 10);
                    }
                }
                else if (orderPosition > -1)
                {
                    this.where = this.sqlString.Substring(wherePosition + 7, orderPosition - wherePosition - 7);
                    this.order = this.sqlString.Substring(orderPosition + 10);
                }
                else
                {
                    this.where = this.sqlString.Substring(wherePosition + 6);
                }
            }
            else if (groupPosition > -1)
            {
                this.from = this.sqlString.Substring(fromPosition + 6, groupPosition - fromPosition - 6);

                if (havingPosition > -1)
                {
                    this.group = this.sqlString.Substring(groupPosition + 10, havingPosition - groupPosition - 10);
                    if (orderPosition > -1)
                    {
                        this.having = this.sqlString.Substring(havingPosition + 8, orderPosition - havingPosition - 8);
                        this.order = this.sqlString.Substring(orderPosition + 10);
                    }
                    else
                    {
                        this.having = this.sqlString.Substring(havingPosition + 8);
                    }
                }
                else if (orderPosition > -1)
                {
                    this.group = this.sqlString.Substring(groupPosition + 10, orderPosition - groupPosition - 10);
                    this.order = this.sqlString.Substring(orderPosition + 10);
                }
                else
                {
                    this.group = this.sqlString.Substring(groupPosition + 10);
                }
            }
            else if (orderPosition > -1)
            {
                this.from = this.sqlString.Substring(fromPosition + 6, orderPosition - fromPosition - 6);
                this.order = this.sqlString.Substring(orderPosition + 10);
            }
            else
            {
                this.from = this.sqlString.Substring(fromPosition + 6);
            }
        }

        private string sqlString;

        private string from;
        /// <summary>
        /// 获取或设置从哪张表取数据
        /// </summary>
        public string From
        {
            get { return this.from; }
            set { this.from = value; }
        }

        private string where;
        /// <summary>
        /// 获取或设置条件
        /// </summary>
        public string Where
        {
            get { return this.where; }
            set { this.where = value; }
        }

        private string column;
        /// <summary>
        /// 获取或设置要读取的字段名
        /// </summary>
        public string Column
        {
            get { return this.column; }
            set { this.column = value; }
        }

        private string order;
        /// <summary>
        /// 获取或设置排序语句
        /// </summary>
        public string Order
        {
            get { return this.order; }
            set { this.order = value; }
        }

        private string group;
        /// <summary>
        /// 获取或设置分组语句
        /// </summary>
        public string Group
        {
            get { return this.group; }
            set { this.group = value; }
        }

        private string having;
        /// <summary>
        /// 获取或设置having语句
        /// </summary>
        public string Having
        {
            get { return this.having; }
            set { this.having = value; }
        }

        private string idField;
        /// <summary>
        /// 获取或设置生成新页时使用的in()中的字段。
        /// </summary>
        public string IdField
        {
            get
            {
                if (string.IsNullOrEmpty(this.idField))
                    this.idField = GetDefaultIdField();
                return this.idField;
            }
            set { this.idField = value; }
        }

        /// <summary>
        /// 获取默认idfiled
        /// </summary>
        /// <returns></returns>
        private string GetDefaultIdField()
        {
            string id = this.column.Split(new char[] { ',' })[0];
            if (string.IsNullOrEmpty(id) || id == "*")
                throw new ArgumentException("Id field error!");
            return id;
        }

        /// <summary>
        /// 返回sql语句
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return GetSqlString();
        }

        private string GetSqlString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select ");
            sb.Append(this.column);
            sb.Append(" from ");
            sb.Append(this.from);
            if (!string.IsNullOrEmpty(this.where))
            {
                sb.Append(" where ");
                sb.Append(this.where);
            }
            if (!string.IsNullOrEmpty(this.group))
            {
                sb.AppendFormat(" group by {0}", this.group);
                if (!string.IsNullOrEmpty(this.having))
                {
                    sb.AppendFormat(" having {0}", this.having);
                }
            }
            if (!string.IsNullOrEmpty(this.order))
            {
                sb.Append(" order by ");
                sb.Append(order);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取sql语句中from位置
        /// </summary>
        /// <param name="sqlString"></param>
        /// <returns></returns>
        public static int GetFromPosition(string sqlString)
        {
            string sql = sqlString.ToLower();
            return GetKeywordPostion(sql, " from ");
        }

        /// <summary>
        /// 获取sql语句中where位置
        /// </summary>
        /// <param name="sqlString"></param>
        /// <returns></returns>
        public static int GetWherePosition(string sqlString)
        {
            string sql = sqlString.ToLower();
            return GetKeywordPostion(sql, " where ");
        }

        private static int GetKeywordPostion(string sqlString, string keyWord)
        {
            MatchCollection matches = Regex.Matches(sqlString, keyWord);
            if (matches.Count == 0)
                return -1;
            else if (matches.Count == 1)
                return matches[0].Index;
            else
            {
                Regex reg1 = new Regex(@"\(");
                Regex reg2 = new Regex(@"\)");
                foreach (Match match in matches)
                {
                    MatchCollection mc1 = reg1.Matches(sqlString.Substring(0, match.Index));
                    MatchCollection mc2 = reg2.Matches(sqlString.Substring(0, match.Index));
                    if (mc1.Count == mc2.Count)
                        return match.Index;
                }
                return -1;
            }
        }
    }
}
