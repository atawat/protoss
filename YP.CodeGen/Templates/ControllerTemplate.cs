﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本: 12.0.0.0
//  
//     对此文件的更改可能会导致不正确的行为。此外，如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
// ------------------------------------------------------------------------------
namespace YP.CodeGen.Templates
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "12.0.0.0")]
    public partial class ControllerTemplate : ControllerTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("using System;\nusing System.Collections.Generic;\nusing System.Linq;\nusing System.N" +
                    "et.Http;\nusing System.Text.RegularExpressions;\nusing System.Web.Http;\nusing Syst" +
                    "em.Web.Http.Cors;\nusing ");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_projectName));
            
            #line default
            #line hidden
            this.Write(".Entity.Model;\nusing ");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_projectName));
            
            #line default
            #line hidden
            this.Write(".Service.");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_tableName));
            
            #line default
            #line hidden
            this.Write(";\nusing YooPoon.Core.Site;\nusing YooPoon.WebFramework.API;\nusing ");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_projectName));
            
            #line default
            #line hidden
            this.Write(".Models;\n\nnamespace ");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_projectName));
            
            #line default
            #line hidden
            this.Write(".Controllers\n{\n\tpublic class ");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_tableName));
            
            #line default
            #line hidden
            this.Write("Controller : ApiController\n\t{\n\t\tprivate readonly I");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_tableName));
            
            #line default
            #line hidden
            this.Write("Service _");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_tableName));
            
            #line default
            #line hidden
            this.Write("Service;\n\n\t\tpublic ");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_tableName));
            
            #line default
            #line hidden
            this.Write("Controller(I");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_tableName));
            
            #line default
            #line hidden
            this.Write("Service ");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_tableName));
            
            #line default
            #line hidden
            this.Write("Service)\n\t\t{\n\t\t\t_");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_tableName));
            
            #line default
            #line hidden
            this.Write("Service = ");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_tableName));
            
            #line default
            #line hidden
            this.Write("Service;\n\t\t}\n\n\t\tpublic ");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_tableName));
            
            #line default
            #line hidden
            this.Write("Model Get(int id)\n\t\t{\n\t\t\tvar entity =_");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_tableName));
            
            #line default
            #line hidden
            this.Write("Service.Get");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_tableName));
            
            #line default
            #line hidden
            this.Write("ById(id);\n\t\t\tvar model = new ");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_tableName));
            
            #line default
            #line hidden
            this.Write("Model\n\t\t\t{\n");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
 foreach(var filed in _entityModels){ if(filed.IsVirtual) continue;
            
            #line default
            #line hidden
            this.Write("\n\t\t\t\t");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(filed.FieldName));
            
            #line default
            #line hidden
            this.Write(" = entity.");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(filed.FieldName));
            
            #line default
            #line hidden
            this.Write("\n");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
}
            
            #line default
            #line hidden
            this.Write("\n\t\t\t}\n\t\t\treturn model;\n\t\t}\n\n\t\tpublic List<");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_tableName));
            
            #line default
            #line hidden
            this.Write("Model> Get(");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_tableName));
            
            #line default
            #line hidden
            this.Write("SearchCondtion condition)\n\t\t{\n\t\t\tvar model = _");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_tableName));
            
            #line default
            #line hidden
            this.Write("Service.Get_");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_tableName));
            
            #line default
            #line hidden
            this.Write("sByConditon(condition).Select(c=>new _");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_tableName));
            
            #line default
            #line hidden
            this.Write("Model\n\t\t\t{\n");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
 foreach(var filed in _entityModels){ if(filed.IsVirtual) continue;
            
            #line default
            #line hidden
            this.Write("\n\t\t\t\t");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(filed.FieldName));
            
            #line default
            #line hidden
            this.Write(" = entity.");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(filed.FieldName));
            
            #line default
            #line hidden
            this.Write("\n");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
}
            
            #line default
            #line hidden
            this.Write("\n\t\t\t});\n\t\t\treturn model;\n\t\t}\n\n\t\tpublic bool Post(");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_tableName));
            
            #line default
            #line hidden
            this.Write("Model model)\n\t\t{\n\t\t\tvar entity = new ");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_tableName));
            
            #line default
            #line hidden
            this.Write("Entity\n\t\t\t{\n");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
 foreach(var filed in _entityModels){ if(filed.FieldName =="Id" || filed.IsVirtual) continue;
            
            #line default
            #line hidden
            this.Write("\n\t\t\t\t");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(filed.FieldName));
            
            #line default
            #line hidden
            this.Write(" = model.");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(filed.FieldName));
            
            #line default
            #line hidden
            this.Write("\n");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
}
            
            #line default
            #line hidden
            this.Write("\n\t\t\t}\n\t\t\tif(_");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_tableName));
            
            #line default
            #line hidden
            this.Write("Service.Create(entity).Id > 0)\n\t\t\t{\n\t\t\t\treturn true;\n\t\t\t}\n\t\t\treturn false;\n\t\t}\n\n\t" +
                    "\tpublic bool Put(");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_tableName));
            
            #line default
            #line hidden
            this.Write("Model model)\n\t\t{\n\t\t\tvar entity = _");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_tableName));
            
            #line default
            #line hidden
            this.Write("Service.Get");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_tableName));
            
            #line default
            #line hidden
            this.Write("ById(model.Id);\n\t\t\tif(entity == null)\n\t\t\t\treturn false;\n");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
 foreach(var filed in _entityModels){ if(filed.FieldName =="Id" || filed.IsVirtual) continue;
            
            #line default
            #line hidden
            this.Write("\n\t\t\tentity.");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(filed.FieldName));
            
            #line default
            #line hidden
            this.Write(" = model.");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(filed.FieldName));
            
            #line default
            #line hidden
            this.Write("\n");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
}
            
            #line default
            #line hidden
            this.Write("\n\t\t\tif(_");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_tableName));
            
            #line default
            #line hidden
            this.Write("Service.Update(entity) != null)\n\t\t\t\treturn true;\n\t\t\treturn false;\n\t\t}\n\n\t\tpublic b" +
                    "ool Delete(int id)\n\t\t{\n\t\t\tvar entity = _");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_tableName));
            
            #line default
            #line hidden
            this.Write("Service.Get");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_tableName));
            
            #line default
            #line hidden
            this.Write("ById(id);\n\t\t\tif(entity == null)\n\t\t\t\treturn false;\n\t\t\tif(_");
            
            #line 6 "D:\Project_Protoss\YP.CodeGen\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_tableName));
            
            #line default
            #line hidden
            this.Write("Service.Delete(entity))\n\t\t\t\treturn true;\n\t\t\treturn false\n\t\t}\n\t}\n}");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "12.0.0.0")]
    public class ControllerTemplateBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
