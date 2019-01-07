using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModDashboard.Calculation
{
    public class CalProvider
    {
        /// <summary>
        /// 由DataTable计算公式
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="parameter">参数名称</param>
        /// <param name="value">参数的实际值</param>
        internal static float Cal(string expression, string parameter, string value)
        {
            expression = expression.Replace(parameter, value);
            return CalcByDataTable(expression);
        }

        /// <summary>
        /// 由DataTable计算公式
        /// </summary>
        /// <param name="expression">表达式</param>
        internal static float CalcByDataTable(string expression)
        {
            object result = new DataTable().Compute(expression, "");
            return float.Parse(result + "");
        }
        ///// <summary>
        ///// 由中序式转换成后序式，再用栈来进行计算
        ///// </summary>
        ///// <param name="expression">表达式</param>
        ///// <returns></returns>
        //public static float CalcByCalcParenthesesExpression(string expression)
        //{
        //    string result = new CalcParenthesesExpression().CalculateParenthesesExpression(expression);
        //    return float.Parse(result);
        //}

        ///// <summary>
        /////  由Microsoft.Eval对象计算表达式，需要引用Microsoft.JScript和Microsoft.Vsa名字空间。
        ///// </summary>
        ///// <param name="expression">表达式</param>
        ///// <returns></returns>
        //public static float CalcByJs(string expression)
        //{
        //    Microsoft.JScript.Vsa.VsaEngine ve = Microsoft.JScript.Vsa.VsaEngine.CreateEngine();
        //    object returnValue = Microsoft.JScript.Eval.JScriptEvaluate((object)expression, ve);
        //    return float.Parse(returnValue.ToString());
        //}
        /// <summary>
        /// 最简单的方式由SQL计算
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        public static float CalcBySQL(string expression)
        {
            string SQL = "SELECT " + expression + " AS RESULT_VALUE";
            SqlConnection conn = new SqlConnection("自己定义连接字符串");
            SqlCommand cmd = new SqlCommand(SQL, conn);
            object o = cmd.ExecuteScalar(); //执行SQL.
            return float.Parse(o.ToString());
        }
    }
}
