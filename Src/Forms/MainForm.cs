using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using ExceptionReflector.Properties;
using System.Linq;

namespace ExceptionReflector
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void SetWaitState()
        {
            this.UseWaitCursor = true;
            this.Cursor = Cursors.WaitCursor;
            foreach (Control ctl in this.Controls)
            {
                ctl.UseWaitCursor = true;
                ctl.Cursor = Cursors.WaitCursor;
                ctl.Enabled = false;
            }
        }

        private void SetAvailableState()
        {
            this.UseWaitCursor = false;
            this.Cursor = Cursors.Arrow;
            foreach (Control ctl in this.Controls)
            {
                ctl.UseWaitCursor = false;
                ctl.Cursor = Cursors.Arrow;
                ctl.Enabled = true;
            }
        }

        private void loadAssemblyLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SetWaitState();

            string filePath = GetFilePath();
            if (!string.IsNullOrWhiteSpace(filePath))
            {
                if (System.IO.File.Exists(filePath))
                    EnumerateAssembly(filePath);
                else
                    MessageBox.Show("File Not Found");
            }

            SetAvailableState();
        }

        private void lnklblClipboardCopy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //TODO: [Backlog] Quando um tipo de exception for private, substituir pelo código de reflection que o recupere e comentado em cima o tipo original. Deixar uma opção configurável para exibir esses excertos comentados ou não. Inserir acima um comentário informando os reiscos de descomentar o bloco (permissão de reflection) e os de não comentar (não tratar essa exception)

            //int cntExceptions = 0;
            string catchs =  "#region [ Catchs ]" + Environment.NewLine;
            foreach (string item in exceptionList.Items)
                catchs +=
                                //"catch (" + item + " x" + (++cntExceptions).ToString() + ")"
                                "catch (" + item + " x)"
                            +
                                Environment.NewLine
                            +
                                "{"
                            +
                                Environment.NewLine
                            +
                                //"\t//Tratamento para a " + item + " x" + cntExceptions.ToString()
                                "\t//Tratamento" //Para facilitar substituição automática por find and replace
                            +
                                Environment.NewLine
                            +
                                "}"
                            +
                                Environment.NewLine;
            catchs += "#endregion";
           Clipboard.Clear();
            Clipboard.SetText(catchs);
        }

        private string GetFilePath()
        {
            var openFileDialong = new OpenFileDialog();

            openFileDialong.Title = Resources.SelectAssemblyLabel;
            openFileDialong.Filter = Resources.SelectAssemblyFilter;
            openFileDialong.FilterIndex = 0;

            if (openFileDialong.ShowDialog(this) == DialogResult.OK)
            {
                this.loadAssemblyLinkLabel.Text = openFileDialong.FileName;
            }

            return openFileDialong.FileName;
        }

        private void EnumerateAssembly(string filePath)
        {
            AssemblyEnumerator assemblyEnumerator = null;
            try { assemblyEnumerator = new AssemblyEnumerator(filePath); }
            catch (Exception x)
            {
                MessageBox.Show("Error trying to load .Net assembly: " + x.Message);
                return;
            }

            foreach (var currentNameSpace in assemblyEnumerator.NameSpaces)
            {
                if (!string.IsNullOrEmpty(currentNameSpace))
                {
                    var namespaceTreeNode = new TreeNode(currentNameSpace);

                    bool hasClasses = false;

                    foreach (var classType in assemblyEnumerator.GetClasses(currentNameSpace))
                    {                        
                        bool hasMethods = false;
                        var classTreeNode = new TreeNode(classType.GetFriendlyName());

                        foreach (var method in assemblyEnumerator.GetMethodds(classType))
                        {
                            var methodBase = method.GetMethodBase();

                                if (methodBase != null)
                                {
                                    var methodTreeNode = new TreeNode(method.GetFriendlyName());
                                    methodTreeNode.Tag = methodBase;

                                    //bool hasExceptions;

                                    //try
                                    //{
                                    //    hasExceptions = method.HasAnyExceptions();
                                    //}
                                    //catch (ArgumentException)
                                    //{
                                    //    hasExceptions = false;
                                    //}

                                    //methodTreeNode.ForeColor = hasExceptions ? Color.Black : Color.DarkGray;
                                    methodTreeNode.ForeColor = Color.Black;
                                    classTreeNode.Nodes.Add(methodTreeNode);
                                    hasMethods = true;
                                }
                        }

                        if (hasMethods)
                        {
                            namespaceTreeNode.Nodes.Add(classTreeNode);
                            hasClasses = true;
                        }

                    }

                    if (hasClasses)
                    {
                        this.itemTreeView.Nodes.Add(namespaceTreeNode);
                    }
                }
            }
        }

        private void ClearExceptionList()
        {
            this.exceptionList.Items.Clear();
        }

        private void PopulateExceptionList(MethodBase method)
        {
            this.exceptionList.Items.Clear();

            var exceptions = method.GetAllExceptions();
            foreach (var exception in exceptions)
            {
                exceptionList.Items.Add(exception.FullName);
            }
        }

        private void itemTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SetWaitState();

            var method = e.Node.Tag as MethodBase;
            if (method != null)
            {
                PopulateExceptionList(method);
            }
            else
            {
                ClearExceptionList();
            }

            SetAvailableState();
        }
    }

    public static class TypeExtensions
    {
        public static string GetFriendlyName(this Type type)
        {
            string tpName = type.Name;

            if (tpName.EndsWith("&"))
                tpName = "ref " + tpName.Substring(0, tpName.Length - 1);

            if (type.IsGenericType)
                tpName = tpName.Substring(0, tpName.Length - 2) + "<" + type.GetGenericArguments().Select(arg => arg.Name).Aggregate((x, y) => x + ", " + y) + ">";

            return tpName;
        }
    }

    public static class MethodInfoExtensions
    {
        public static string GetFriendlyName(this MethodInfo methodInfo)
        {
            StringBuilder sb = new StringBuilder(1000);

            //if (methodInfo.IsPrivate)
            //    sb.Append("private ");
            //else if (methodInfo.IsPublic)
            //    sb.Append("public ");

            //if (methodInfo.IsStatic)
            //    sb.Append("static ");

            //if (methodInfo.IsAbstract)
            //    sb.Append("abstract ");

            //if (methodInfo.IsVirtual)
            //    sb.Append("virtual ");

            //sb.AppendFormat("{0} {1}", methodInfo.ReturnType.GetFriendlyName(), methodInfo.Name);

            sb.Append(methodInfo.Name);

            if (methodInfo.IsGenericMethod)
                sb.AppendFormat("<{0}>", methodInfo.GetGenericArguments().Select(arg => arg.Name).Aggregate((x, y) => x + ", " + y));

            List<String> parameterDescriptions = new List<string>();
            foreach (ParameterInfo parameter in methodInfo.GetParameters())
            {
                parameterDescriptions.Add(string.Format("{0} {1} {2}", ((parameter.IsIn) ? ("in") : ((parameter.IsOut) ? ("out") : (string.Empty))), parameter.ParameterType.GetFriendlyName(), parameter.Name));
            }

            sb.AppendFormat("({0})", string.Join(", ", parameterDescriptions));
            
            return sb.ToString();
        }

        public static MethodBase GetMethodBase( this MethodInfo method)
        {
            try
            {
                return MethodBase.GetMethodFromHandle(method.MethodHandle);
            }
            catch (ArgumentException)
            {
                return null;
            }
        }
    }
}
