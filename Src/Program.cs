using System;
using System.Windows.Forms;

namespace ExceptionReflector
{
    static class Program
    {
        static void UnhandledExcptEvtHndlr(object sender, UnhandledExceptionEventArgs e)
        {
            string msg =
                                "ATENÇÃO: OCORREU UM ERRO QUE NÃO PODE SER TRATADO E O APLICATIVO SERÁ FECHADO!!!"
                            +
                                Environment.NewLine
                            +
                                "Caso o problema persista, contate o suporte passando as informações abaixo."
                            +
                                Environment.NewLine
                            +
                                "Informações que puderam ser recuperadas:";
            if (sender != null)
                msg +=
                                Environment.NewLine
                            +
                                "sender: [" + sender.ToString() + "]";
            if (e != null)
            {
                msg +=
                                Environment.NewLine
                            +
                                "e: [" + e.ToString() + "]"
                            +
                                Environment.NewLine
                            +
                                "e.IsTerminating: [" + e.IsTerminating.ToString() + "]";
                if (e.ExceptionObject != null)
                    msg +=
                                Environment.NewLine
                            +
                                "e.ExceptionObject: [" + e.ExceptionObject.ToString() + "]";
            }
            MessageBox.Show(msg, "*** EXCEÇÃO FATAL ***", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledExcptEvtHndlr);

            //TypeHierarchy.Node n00 = new TypeHierarchy.Node(typeof(Type));
            //TypeHierarchy.Node n01 = new TypeHierarchy.Node(typeof(int));
            //TypeHierarchy.Node n02 = null;
            //TypeHierarchy.Node n03 = null;
            //TypeHierarchy.Node n04 = new TypeHierarchy.Node(typeof(Program));
            //TypeHierarchy.Node n05 = new TypeHierarchy.Node(typeof(long));
            //TypeHierarchy.Node n06 = n00;
            //TypeHierarchy.Node n07 = n01;
            //TypeHierarchy.Node n08 = n03;
            //TypeHierarchy.Node n09 = new TypeHierarchy.Node(typeof(Type));
            //TypeHierarchy.Node n10 = new TypeHierarchy.Node(typeof(int)); ;
            //TypeHierarchy.Node n11 = n04;
            //TypeHierarchy.Node n12 = n05;
            //TypeHierarchy.Node n13 = new TypeHierarchy.Node(null);
            //TypeHierarchy.Node n14 = new TypeHierarchy.Node(null);

            ////Reference com value
            //bool t000 = n00.Equals(n01);
            //bool t001 = ((object)n00).Equals(n01);
            //bool t002 = n00.Equals(((object)n01));
            //bool t003 = Object.Equals(n00, n01);
            //bool t004 = n00 == n01;
            //bool t005 = ((object)n00) == n01;
            //bool t006 = n00 != n01;
            //bool t007 = n00 != ((object)n01);

            ////Reference com null
            //bool t010 = n00.Equals(n02);
            //bool t011 = ((object)n00).Equals(n02);
            //bool t012 = n00.Equals(((object)n02));
            //bool t013 = Object.Equals(n00, n02);
            //bool t014 = n00 == n02;
            //bool t015 = ((object)n00) == n02;
            //bool t016 = n00 != n02;
            //bool t017 = n00 != ((object)n02);

            ////value com null
            //bool t020 = n01.Equals(n02);
            //bool t021 = ((object)n01).Equals(n02);
            //bool t022 = n01.Equals(((object)n02));
            //bool t023 = Object.Equals(n01, n02);
            //bool t024 = n01 == n02;
            //bool t025 = ((object)n01) == n02;
            //bool t026 = n01 != n02;
            //bool t027 = n01 != ((object)n02);

            ////null com null
            //bool t033 = Object.Equals(n03, n02);
            //bool t034 = n03 == n02;
            //bool t035 = ((object)n03) == n02;
            //bool t036 = n03 != n02;
            //bool t037 = n03 != ((object)n02);

            ////Reference com reference diferentes
            //bool t040 = n00.Equals(n04);
            //bool t041 = ((object)n00).Equals(n04);
            //bool t042 = n00.Equals(((object)n04));
            //bool t043 = Object.Equals(n00, n04);
            //bool t044 = n00 == n04;
            //bool t045 = ((object)n00) == n04;
            //bool t046 = n00 != n04;
            //bool t047 = n00 != ((object)n04);

            ////value com value diferentes
            //bool t050 = n05.Equals(n01);
            //bool t051 = ((object)n05).Equals(n01);
            //bool t052 = n05.Equals(((object)n01));
            //bool t053 = Object.Equals(n05, n01);
            //bool t054 = n05 == n01;
            //bool t055 = ((object)n05) == n01;
            //bool t056 = n05 != n01;
            //bool t057 = n05 != ((object)n01);

            ////Reference com referência para mesma reference
            //bool t060 = n00.Equals(n06);
            //bool t061 = ((object)n00).Equals(n06);
            //bool t062 = n00.Equals(((object)n06));
            //bool t063 = Object.Equals(n00, n06);
            //bool t064 = n00 == n06;
            //bool t065 = ((object)n00) == n06;
            //bool t066 = n00 != n06;
            //bool t067 = n00 != ((object)n06);

            ////Reference com referência para value
            //bool t070 = n00.Equals(n07);
            //bool t071 = ((object)n00).Equals(n07);
            //bool t072 = n00.Equals(((object)n07));
            //bool t073 = Object.Equals(n00, n07);
            //bool t074 = n00 == n07;
            //bool t075 = ((object)n00) == n07;
            //bool t076 = n00 != n07;
            //bool t077 = n00 != ((object)n07);

            ////Reference com referência para null
            //bool t080 = n00.Equals(n08);
            //bool t081 = ((object)n00).Equals(n08);
            //bool t082 = n00.Equals(((object)n08));
            //bool t083 = Object.Equals(n00, n08);
            //bool t084 = n00 == n08;
            //bool t085 = ((object)n00) == n08;
            //bool t086 = n00 != n08;
            //bool t087 = n00 != ((object)n08);

            ////value com referência para reference
            //bool t090 = n01.Equals(n06);
            //bool t091 = ((object)n01).Equals(n06);
            //bool t092 = n01.Equals(((object)n06));
            //bool t093 = Object.Equals(n01, n06);
            //bool t094 = n01 == n06;
            //bool t095 = ((object)n01) == n06;
            //bool t096 = n01 != n06;
            //bool t097 = n01 != ((object)n06);

            ////value com referência para mesma value
            //bool t100 = n01.Equals(n07);
            //bool t101 = ((object)n01).Equals(n07);
            //bool t102 = n01.Equals(((object)n07));
            //bool t103 = Object.Equals(n01, n07);
            //bool t104 = n01 == n07;
            //bool t105 = ((object)n01) == n07;
            //bool t106 = n01 != n07;
            //bool t107 = n01 != ((object)n07);

            ////value com referência para null
            //bool t110 = n01.Equals(n08);
            //bool t111 = ((object)n01).Equals(n08);
            //bool t112 = n01.Equals(((object)n08));
            //bool t113 = Object.Equals(n01, n08);
            //bool t114 = n01 == n08;
            //bool t115 = ((object)n01) == n08;
            //bool t116 = n01 != n08;
            //bool t117 = n01 != ((object)n08);

            ////null com referência para reference
            //bool t123 = Object.Equals(n02, n06);
            //bool t124 = n02 == n06;
            //bool t125 = ((object)n02) == n06;
            //bool t126 = n02 != n06;
            //bool t127 = n02 != ((object)n06);

            ////null com referência para value
            //bool t133 = Object.Equals(n02, n07);
            //bool t134 = n02 == n07;
            //bool t135 = ((object)n02) == n07;
            //bool t136 = n02 != n07;
            //bool t137 = n02 != ((object)n07);

            ////null com referência para outro null
            //bool t143 = Object.Equals(n02, n08);
            //bool t144 = n02 == n08;
            //bool t145 = ((object)n02) == n08;
            //bool t146 = n02 != n08;
            //bool t147 = n02 != ((object)n08);

            ////null com referência para mesmo null
            //bool t153 = Object.Equals(n03, n08);
            //bool t154 = n03 == n08;
            //bool t155 = ((object)n03) == n08;
            //bool t156 = n03 != n08;
            //bool t157 = n03 != ((object)n08);

            ////Reference com outro reference igual
            //bool t160 = n00.Equals(n09);
            //bool t161 = ((object)n00).Equals(n09);
            //bool t162 = n00.Equals(((object)n09));
            //bool t163 = Object.Equals(n00, n09);
            //bool t164 = n00 == n09;
            //bool t165 = ((object)n00) == n09;
            //bool t166 = n00 != n09;
            //bool t167 = n00 != ((object)n09);

            ////value com outro value igual
            //bool t170 = n01.Equals(n10);
            //bool t171 = ((object)n01).Equals(n10);
            //bool t172 = n01.Equals(((object)n10));
            //bool t173 = Object.Equals(n01, n10);
            //bool t174 = n01 == n10;
            //bool t175 = ((object)n01) == n10;
            //bool t176 = n01 != n10;
            //bool t177 = n01 != ((object)n10);

            ////Reference com referencia para reference diferente
            //bool t180 = n00.Equals(n11);
            //bool t181 = ((object)n00).Equals(n11);
            //bool t182 = n00.Equals(((object)n11));
            //bool t183 = Object.Equals(n00, n11);
            //bool t184 = n00 == n11;
            //bool t185 = ((object)n00) == n11;
            //bool t186 = n00 != n11;
            //bool t187 = n00 != ((object)n11);

            ////value com referencia para value diferente
            //bool t190 = n01.Equals(n12);
            //bool t191 = ((object)n01).Equals(n12);
            //bool t192 = n01.Equals(((object)n12));
            //bool t193 = Object.Equals(n01, n12);
            //bool t194 = n01 == n12;
            //bool t195 = ((object)n01) == n12;
            //bool t196 = n01 != n12;
            //bool t197 = n01 != ((object)n12);

            ////Reference com ele mesmo
            //bool t200 = n00.Equals(n00);
            //bool t201 = ((object)n00).Equals(n00);
            //bool t202 = n00.Equals(((object)n00));
            //bool t203 = Object.Equals(n00, n00);
            //bool t204 = n00 == n00;
            //bool t205 = ((object)n00) == n00;
            //bool t206 = n00 != n00;
            //bool t207 = n00 != ((object)n00);

            ////value com ele mesmo
            //bool t210 = n01.Equals(n01);
            //bool t211 = ((object)n01).Equals(n01);
            //bool t212 = n01.Equals(((object)n01));
            //bool t213 = Object.Equals(n01, n01);
            //bool t214 = n01 == n01;
            //bool t215 = ((object)n01) == n01;
            //bool t216 = n01 != n01;
            //bool t217 = n01 != ((object)n01);

            ////null com null
            //bool t223 = Object.Equals(n02, n02);
            //bool t224 = n02 == n02;
            //bool t225 = ((object)n02) == n02;
            //bool t226 = n02 != n02;
            //bool t227 = n02 != ((object)n02);

            ////Reference com node contendo null
            //bool t230 = n00.Equals(n13);
            //bool t231 = ((object)n00).Equals(n13);
            //bool t232 = n00.Equals(((object)n13));
            //bool t233 = Object.Equals(n00, n13);
            //bool t234 = n00 == n13;
            //bool t235 = ((object)n00) == n13;
            //bool t236 = n00 != n13;
            //bool t237 = n00 != ((object)n13);

            ////Node contendo Null com null
            //bool t240 = n13.Equals(n02);
            //bool t241 = ((object)n13).Equals(n02);
            //bool t242 = n13.Equals(((object)n02));
            //bool t243 = Object.Equals(n13, n02);
            //bool t244 = n13 == n02;
            //bool t245 = ((object)n13) == n02;
            //bool t246 = n13 != n02;
            //bool t247 = n13 != ((object)n02);

            ////value com Node contendo null
            //bool t250 = n01.Equals(n13);
            //bool t251 = ((object)n01).Equals(n13);
            //bool t252 = n01.Equals(((object)n13));
            //bool t253 = Object.Equals(n01, n13);
            //bool t254 = n01 == n13;
            //bool t255 = ((object)n01) == n13;
            //bool t256 = n01 != n13;
            //bool t257 = n01 != ((object)n13);

            ////Node contendo null com ele mesmo
            //bool t260 = n13.Equals(n13);
            //bool t261 = ((object)n13).Equals(n13);
            //bool t262 = n13.Equals(((object)n13));
            //bool t263 = Object.Equals(n13, n13);
            //bool t264 = n13 == n13;
            //bool t265 = ((object)n13) == n13;
            //bool t266 = n13 != n13;
            //bool t267 = n13 != ((object)n13);

            ////Node contendo null com outro node contendo null
            //bool t270 = n14.Equals(n13);
            //bool t271 = ((object)n14).Equals(n13);
            //bool t272 = n14.Equals(((object)n13));
            //bool t273 = Object.Equals(n14, n13);
            //bool t274 = n14 == n13;
            //bool t275 = ((object)n14) == n13;
            //bool t276 = n14 != n13;
            //bool t277 = n14 != ((object)n13); ;

            //System.Diagnostics.Debugger.Break();

            //TypeHierarchy x = new TypeHierarchy();
            //x.Root.AddSubType(typeof(System.IO.FileNotFoundException));
            //x.Root.AddSubType(typeof(System.IO.DirectoryNotFoundException));
            //x.Root.AddSubType(typeof(System.ApplicationException));
            //x.Root.AddSubType(typeof(System.Exception));
            //x.Root.AddSubType(typeof(System.ExecutionEngineException));
            //x.Root.AddSubType(typeof(System.FieldAccessException));
            //x.Root.AddSubType(typeof(System.FormatException));
            //x.Root.AddSubType(typeof(System.SystemException));
            //x.Root.AddSubType(typeof(System.OutOfMemoryException));
            //x.Root.AddSubType(typeof(System.ArgumentException));
            //x.Root.AddSubType(typeof(System.ArgumentNullException));
            //x.Root.AddSubType(typeof(System.ArgumentOutOfRangeException));
            //x.Root.AddSubType(typeof(System.ArithmeticException));
            //x.Root.AddSubType(typeof(DivideByZeroException));
            //x.Root.AddSubType(typeof(System.Runtime.Serialization.SerializationException));
            //x.Root.AddSubType(typeof(System.Runtime.Remoting.RemotingException));
            //x.Root.AddSubType(typeof(System.Runtime.Remoting.RemotingTimeoutException));
            //x.Root.AddSubType(typeof(System.Reflection.CustomAttributeFormatException));
            //x.Root.AddSubType(typeof(System.Reflection.InvalidFilterCriteriaException));
            //x.Root.AddSubType(typeof(System.Reflection.TargetInvocationException));
            //x.Root.AddSubType(typeof(System.Reflection.TargetParameterCountException));
            //x.Root.AddSubType(typeof(System.Threading.LockRecursionException));
            //x.Root.AddSubType(typeof(System.Threading.SemaphoreFullException));
            //x.Root.AddSubType(typeof(System.Threading.ThreadAbortException));
            //x.Root.AddSubType(typeof(System.Threading.ThreadInterruptedException));
            //x.Root.AddSubType(typeof(System.Threading.ThreadStartException));
            //x.Root.AddSubType(typeof(System.Threading.ThreadStateException));
            //x.Root.AddSubType(typeof(System.Threading.WaitHandleCannotBeOpenedException));
            //x.Root.AddSubType(typeof(System.Xml.XmlException));
            //x.Root.AddSubType(typeof(System.Security.SecurityException));
            //x.Root.AddSubType(typeof(System.Security.VerificationException));
            //x.Root.AddSubType(typeof(System.Security.XmlSyntaxException));
            //x.Root.AddSubType(typeof(System.Security.Policy.PolicyException));
            //x.Root.AddSubType(typeof(System.OverflowException));
            //x.Root.AddSubType(typeof(System.OperationCanceledException));
            //x.Root.AddSubType(typeof(System.ObjectDisposedException));
            //x.Root.AddSubType(typeof(System.NullReferenceException));
            //x.Root.AddSubType(typeof(System.NotSupportedException));
            //x.Root.AddSubType(typeof(System.NotImplementedException));
            //x.Root.AddSubType(typeof(System.NotFiniteNumberException));
            //x.Root.AddSubType(typeof(System.MulticastNotSupportedException));
            //x.Root.AddSubType(typeof(System.MissingMethodException));
            //x.Root.AddSubType(typeof(System.MissingMemberException));
            //x.Root.AddSubType(typeof(System.MissingFieldException));
            //x.Root.AddSubType(typeof(System.MethodAccessException));
            //x.Root.AddSubType(typeof(System.MemberAccessException));
            //x.Root.AddSubType(typeof(System.Management.Instrumentation.InstanceNotFoundException));
            //x.Root.AddSubType(typeof(System.Management.Instrumentation.InstrumentationBaseException));
            //x.Root.AddSubType(typeof(System.Management.Instrumentation.InstrumentationException));
            //x.Root.AddSubType(typeof(System.IO.DriveNotFoundException));
            //x.Root.AddSubType(typeof(System.IO.EndOfStreamException));
            //x.Root.AddSubType(typeof(System.IO.IOException));
            //x.Root.AddSubType(typeof(System.IO.FileLoadException));
            //x.Root.AddSubType(typeof(System.IO.FileNotFoundException));
            //x.Root.AddSubType(typeof(System.IO.InternalBufferOverflowException));
            //x.Root.AddSubType(typeof(System.InvalidTimeZoneException));
            //x.Root.AddSubType(typeof(System.InvalidProgramException));
            //x.Root.AddSubType(typeof(System.InvalidOperationException));
            //x.Root.AddSubType(typeof(System.InvalidCastException));
            //x.Root.AddSubType(typeof(System.InsufficientMemoryException));
            //x.Root.AddSubType(typeof(System.InsufficientExecutionStackException));
            //x.Root.AddSubType(typeof(System.IndexOutOfRangeException));
            //x.Root.AddSubType(typeof(System.Globalization.CultureNotFoundException));
            //x.Root.AddSubType(typeof(System.EntryPointNotFoundException));
            //x.Root.AddSubType(typeof(System.DuplicateWaitObjectException));
            //x.Root.AddSubType(typeof(System.DataMisalignedException));
            //x.Root.AddSubType(typeof(System.Data.SqlClient.SqlException));
            //x.Root.AddSubType(typeof(BadImageFormatException));

            //string tst1 = x.ToString();

            ////System.Diagnostics.Debugger.Break();

            //ExceptionHierarchy y = new ExceptionHierarchy();

            ////Exceptions Báscias que escondem muitas outras
            //y.AddForbidenException(typeof(Exception));
            //y.AddForbidenException(typeof(SystemException));
            //y.AddForbidenException(typeof(ApplicationException));
            ////Teste de inserção de exception repetida
            //y.AddForbidenException(typeof(Exception));

            //y.AddThrownException(typeof(System.IO.FileNotFoundException));
            //y.AddThrownException(typeof(System.IO.DirectoryNotFoundException));
            //y.AddThrownException(typeof(System.ApplicationException));
            //y.AddThrownException(typeof(System.Exception));
            //y.AddThrownException(typeof(System.ExecutionEngineException));
            //y.AddThrownException(typeof(System.FieldAccessException));
            //y.AddThrownException(typeof(System.FormatException));
            //y.AddThrownException(typeof(System.SystemException));
            //y.AddThrownException(typeof(System.OutOfMemoryException));
            //y.AddThrownException(typeof(System.ArgumentException));
            //y.AddThrownException(typeof(System.ArgumentNullException));
            //y.AddThrownException(typeof(System.ArgumentOutOfRangeException));
            //y.AddThrownException(typeof(System.ArithmeticException));
            //y.AddThrownException(typeof(DivideByZeroException));
            //y.AddThrownException(typeof(System.Runtime.Serialization.SerializationException));
            //y.AddThrownException(typeof(System.Runtime.Remoting.RemotingException));
            //y.AddThrownException(typeof(System.Runtime.Remoting.RemotingTimeoutException));
            //y.AddThrownException(typeof(System.Reflection.CustomAttributeFormatException));
            //y.AddThrownException(typeof(System.Reflection.InvalidFilterCriteriaException));
            //y.AddThrownException(typeof(System.Reflection.TargetInvocationException));
            //y.AddThrownException(typeof(System.Reflection.TargetParameterCountException));
            //y.AddThrownException(typeof(System.Threading.LockRecursionException));
            //y.AddThrownException(typeof(System.Threading.SemaphoreFullException));
            //y.AddThrownException(typeof(System.Threading.ThreadAbortException));
            //y.AddThrownException(typeof(System.Threading.ThreadInterruptedException));
            //y.AddThrownException(typeof(System.Threading.ThreadStartException));
            //y.AddThrownException(typeof(System.Threading.ThreadStateException));
            //y.AddThrownException(typeof(System.Threading.WaitHandleCannotBeOpenedException));
            //y.AddThrownException(typeof(System.Xml.XmlException));
            //y.AddThrownException(typeof(System.Security.SecurityException));
            //y.AddThrownException(typeof(System.Security.VerificationException));
            //y.AddThrownException(typeof(System.Security.XmlSyntaxException));
            //y.AddThrownException(typeof(System.Security.Policy.PolicyException));
            //y.AddThrownException(typeof(System.OverflowException));
            //y.AddThrownException(typeof(System.OperationCanceledException));
            //y.AddThrownException(typeof(System.ObjectDisposedException));
            //y.AddThrownException(typeof(System.NullReferenceException));
            //y.AddThrownException(typeof(System.NotImplementedException));
            //y.AddThrownException(typeof(System.NotFiniteNumberException));
            //y.AddThrownException(typeof(System.MulticastNotSupportedException));
            //y.AddThrownException(typeof(System.MissingMethodException));
            //y.AddThrownException(typeof(System.MissingMemberException));
            //y.AddThrownException(typeof(System.MissingFieldException));
            //y.AddThrownException(typeof(System.MethodAccessException));
            //y.AddThrownException(typeof(System.MemberAccessException));
            //y.AddThrownException(typeof(System.Management.Instrumentation.InstanceNotFoundException));
            //y.AddThrownException(typeof(System.Management.Instrumentation.InstrumentationBaseException));
            //y.AddThrownException(typeof(System.Management.Instrumentation.InstrumentationException));
            //y.AddThrownException(typeof(System.IO.DriveNotFoundException));
            //y.AddThrownException(typeof(System.IO.EndOfStreamException));
            //y.AddThrownException(typeof(System.IO.IOException));
            //y.AddThrownException(typeof(System.IO.FileLoadException));
            //y.AddThrownException(typeof(System.IO.FileNotFoundException));
            //y.AddThrownException(typeof(System.IO.InternalBufferOverflowException));
            //y.AddThrownException(typeof(System.InvalidTimeZoneException));
            //y.AddThrownException(typeof(System.InvalidProgramException));
            //y.AddThrownException(typeof(System.InvalidOperationException));
            //y.AddThrownException(typeof(System.InvalidCastException));
            //y.AddThrownException(typeof(System.InsufficientMemoryException));
            //y.AddThrownException(typeof(System.InsufficientExecutionStackException));
            //y.AddThrownException(typeof(System.IndexOutOfRangeException));
            //y.AddThrownException(typeof(System.Globalization.CultureNotFoundException));
            //y.AddThrownException(typeof(System.EntryPointNotFoundException));
            //y.AddThrownException(typeof(System.DuplicateWaitObjectException));
            //y.AddThrownException(typeof(System.DataMisalignedException));
            //y.AddThrownException(typeof(System.Data.SqlClient.SqlException));
            //y.AddThrownException(typeof(BadImageFormatException));
            //y.AddThrownException(typeof(System.IO.FileNotFoundException));


            //string tst2 = y.ToString();

            //System.Diagnostics.Debugger.Break();

            //TODO: Unit tests

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
