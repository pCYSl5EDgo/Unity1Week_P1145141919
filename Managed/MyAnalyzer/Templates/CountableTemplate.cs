﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン: 16.0.0.0
//  
//     このファイルへの変更は、正しくない動作の原因になる可能性があり、
//     コードが再生成されると失われます。
// </auto-generated>
// ------------------------------------------------------------------------------
namespace MyAnalyzer.Templates
{
    using Microsoft.CodeAnalysis;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public partial class CountableTemplate : CountableTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("namespace ");
            
            #line 7 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(TypeSymbol.ContainingNamespace.ToDisplayString()));
            
            #line default
            #line hidden
            this.Write("\r\n{\r\n    partial struct ");
            
            #line 9 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(TypeSymbol.Name));
            
            #line default
            #line hidden
            this.Write("\r\n    {\r\n        public partial struct Countable : global::System.IDisposable\r\n  " +
                    "      {\r\n            public global::Unity.Collections.NativeArray<int> Count;\r\n");
            
            #line 14 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
 var list = new List<IFieldSymbol>();
foreach (var member in TypeSymbol.GetMembers()) {
    if (member.IsStatic) continue;
    var fieldSymbol = member as IFieldSymbol;
    if (fieldSymbol == null) continue;
    var fieldSymbolType = fieldSymbol.Type as INamedTypeSymbol;
    if (fieldSymbolType == null) continue;
    list.Add(fieldSymbol); 
            
            #line default
            #line hidden
            this.Write("            public global::Unity.Collections.NativeArray<global::");
            
            #line 22 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(fieldSymbolType.ContainingNamespace.ToDisplayString()));
            
            #line default
            #line hidden
            this.Write(".");
            
            #line 22 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(fieldSymbolType.Name));
            
            #line default
            #line hidden
            this.Write(".Eight> ");
            
            #line 22 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(fieldSymbol.Name));
            
            #line default
            #line hidden
            this.Write("Array;\r\n");
            
            #line 23 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write("\r\n            public int Capacity => ");
            
            #line 25 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(list[0].Name));
            
            #line default
            #line hidden
            this.Write("Array.Length;\r\n\r\n            public int ChunkCount => ((Count[0] - 1) >> 3) + 1;\r" +
                    "\n\r\n            public void EnsureCapacity(int newCapacity)\r\n            {\r\n     " +
                    "           if (Capacity >= newCapacity) return;\r\n\r\n");
            
            #line 33 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
 foreach (var fieldSymbol in list) { 
            
            #line default
            #line hidden
            this.Write("                {\r\n                    var tmp = new global::Unity.Collections.Na" +
                    "tiveArray<global::");
            
            #line 35 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(fieldSymbol.Type.ContainingNamespace.ToDisplayString()));
            
            #line default
            #line hidden
            this.Write(".");
            
            #line 35 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(fieldSymbol.Type.Name));
            
            #line default
            #line hidden
            this.Write(".Eight>(newCapacity, global::Unity.Collections.Allocator.Persistent);\r\n          " +
                    "          ");
            
            #line 36 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(fieldSymbol.Name));
            
            #line default
            #line hidden
            this.Write("Array.CopyTo(tmp);\r\n                    ");
            
            #line 37 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(fieldSymbol.Name));
            
            #line default
            #line hidden
            this.Write("Array.Dispose();\r\n                    ");
            
            #line 38 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(fieldSymbol.Name));
            
            #line default
            #line hidden
            this.Write("Array = tmp;\r\n                }\r\n");
            
            #line 40 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write("            }\r\n\r\n            public void Dispose()\r\n            {\r\n              " +
                    "  Count.Dispose();\r\n");
            
            #line 46 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
 foreach (var fieldSymbol in list) { 
            
            #line default
            #line hidden
            this.Write("                ");
            
            #line 47 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(fieldSymbol.Name));
            
            #line default
            #line hidden
            this.Write("Array.Dispose();\r\n");
            
            #line 48 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write("            }\r\n\r\n            public Countable(int count)\r\n            {\r\n        " +
                    "        Count = new global::Unity.Collections.NativeArray<int>(1, global::Unity." +
                    "Collections.Allocator.Persistent);\r\n                var capacity = ((count - 1) " +
                    ">> 3) + 1;\r\n");
            
            #line 55 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
 foreach (var fieldSymbol in list) { 
            
            #line default
            #line hidden
            this.Write("                ");
            
            #line 56 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(fieldSymbol.Name));
            
            #line default
            #line hidden
            this.Write("Array = new global::Unity.Collections.NativeArray<global::");
            
            #line 56 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(fieldSymbol.Type.ContainingNamespace.ToDisplayString()));
            
            #line default
            #line hidden
            this.Write(".");
            
            #line 56 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(fieldSymbol.Type.Name));
            
            #line default
            #line hidden
            this.Write(".Eight>(capacity, global::Unity.Collections.Allocator.Persistent);\r\n");
            
            #line 57 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write("            }\r\n");
            
            #line 59 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
 var sortable = AttributeData.ConstructorArguments.Length != 0;
  if (sortable) {
    var additionalMemberTypes = AttributeData.ConstructorArguments[0].Values;
    var additionalArgumentMemberNames = AttributeData.ConstructorArguments[1].Values;
    var additionalFieldMemberNames = AttributeData.ConstructorArguments[2].Values;
    var isAliveFunctionString = AttributeData.ConstructorArguments[3].Value as string; 
            
            #line default
            #line hidden
            this.Write("\r\n            public bool IsAlive(int index");
            
            #line 66 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
 for (var i = 0; i < additionalArgumentMemberNames.Length; i++) { 
        var paramType = additionalMemberTypes[i].Value as INamedTypeSymbol;
        var paramName = additionalArgumentMemberNames[i].Value as string;
        var isNotSpecial = paramType.SpecialType == SpecialType.None; 
            
            #line default
            #line hidden
            this.Write(", ");
            
            #line 69 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
 if (isNotSpecial){ 
            
            #line default
            #line hidden
            this.Write("global::");
            
            #line 69 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 69 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(paramType.ToDisplayString()));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 69 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(paramName));
            
            #line default
            #line hidden
            
            #line 69 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write(")\r\n            {\r\n                ");
            
            #line 71 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(isAliveFunctionString));
            
            #line default
            #line hidden
            this.Write("\r\n            }\r\n        }\r\n\r\n        [global::Unity.Burst.BurstCompile]\r\n       " +
                    " public partial struct SortJob : global::Unity.Jobs.IJob\r\n        {\r\n           " +
                    " public Countable This;\r\n");
            
            #line 79 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
 for (var i = 0; i < additionalFieldMemberNames.Length; i++) { 
      var fieldType = additionalMemberTypes[i].Value as INamedTypeSymbol;
      var fieldName = additionalFieldMemberNames[i].Value as string;
      var isNotSpecial = fieldType.SpecialType == SpecialType.None; 
            
            #line default
            #line hidden
            this.Write("            public ");
            
            #line 83 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
 if (isNotSpecial){ 
            
            #line default
            #line hidden
            this.Write("global::");
            
            #line 83 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 83 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(fieldType.ToDisplayString()));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 83 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(fieldName));
            
            #line default
            #line hidden
            this.Write(";\r\n");
            
            #line 84 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write("\r\n            public void Execute()\r\n            {\r\n                var ____count" +
                    " = This.Count[0];\r\n                for (var ____index = 0; ____index < ____count" +
                    "; )\r\n                {\r\n                    if (This.IsAlive(____index");
            
            #line 91 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
 for (var i = 0; i < additionalFieldMemberNames.Length; i++) { var fieldName = additionalFieldMemberNames[i].Value as string; 
            
            #line default
            #line hidden
            this.Write(", this.");
            
            #line 91 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(fieldName));
            
            #line default
            #line hidden
            
            #line 91 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write("))\r\n                    {\r\n                        ++____index;\r\n                " +
                    "        continue;\r\n                    }\r\n\r\n                    while (!This.IsA" +
                    "live(--____count");
            
            #line 97 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
 for (var i = 0; i < additionalFieldMemberNames.Length; i++) { var fieldName = additionalFieldMemberNames[i].Value as string; 
            
            #line default
            #line hidden
            this.Write(", this.");
            
            #line 97 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(fieldName));
            
            #line default
            #line hidden
            
            #line 97 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write(@"))
                    {
                        if (____index >= ____count)
                        {
                            goto END;
                        }
                    }

                    var ____srcIndex = ____index >> 3;
                    var ____srcColumn = (____index & 7) >> 2;
                    var ____srcRow = ____index & 3;
                    var ____dstIndex = ____count >> 3;
                    var ____dstColumn = (____count & 7) >> 2;
                    var ____dstRow = ____count & 3;

");
            
            #line 112 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
 foreach (var fieldSymbol in list) { 
            
            #line default
            #line hidden
            this.Write("                    {\r\n                        var ____dst = This.");
            
            #line 114 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(fieldSymbol.Name));
            
            #line default
            #line hidden
            this.Write("Array[____dstIndex];\r\n                        var ____src = This.");
            
            #line 115 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(fieldSymbol.Name));
            
            #line default
            #line hidden
            this.Write("Array[____srcIndex];\r\n                        ____src.CopyToDestination(____srcCo" +
                    "lumn, ____srcRow, ref ____dst, ____dstColumn, ____dstRow);\r\n                    " +
                    "    This.");
            
            #line 117 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(fieldSymbol.Name));
            
            #line default
            #line hidden
            this.Write("Array[____dstIndex] = ____dst;\r\n                    }\r\n");
            
            #line 119 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write("                }\r\n\r\n            END:\r\n                This.Count[0] = ____count;" +
                    "\r\n            }\r\n        }\r\n");
            
            #line 126 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
 } else {
            
            #line default
            #line hidden
            this.Write("        }\r\n");
            
            #line 128 "C:\Users\conve\source\repos\Unity1Week_P1145141919\Managed\MyAnalyzer\Templates\CountableTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write("    }\r\n}\r\n");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public class CountableTemplateBase
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
