﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KD.Plugins.MobiscriptEditor.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("KD.Plugins.MobiscriptEditor.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to @VAR	allows to define a variable name and its value. @var (Variable name = 100)
        ///@DEC	designates the 2D catalog CAT2D_GENDEC in a model script. @DEC (RED = CHEV4X)
        ///@COMMENT	example @comment (Hello every body)
        ///@INTERFACE	allows a wizard to store its own information.
        ///@EXE	used to define an executable serving as a configurator.
        ///@DLL	allows to call the ObjectWizard () function of a dll.
        ///@URL	to call a url on a block in InSitu
        ///@2D	Name of a 2D to use: code of a block, name of a 2D entity or name of a wmf o [rest of string was truncated]&quot;;.
        /// </summary>
        public static string BlockScriptKeywords {
            get {
                return ResourceManager.GetString("BlockScriptKeywords", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cancel.
        /// </summary>
        public static string CancelButtonTitle {
            get {
                return ResourceManager.GetString("CancelButtonTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to abstract
        ///arguments
        ///await
        ///boolean
        ///break
        ///byte
        ///case
        ///catch
        ///char
        ///class
        ///const
        ///continue
        ///debugger
        ///default
        ///delete
        ///do
        ///double
        ///else
        ///enum
        ///eval
        ///export
        ///extends
        ///false
        ///final
        ///finally
        ///float
        ///for
        ///function
        ///goto
        ///if
        ///implements
        ///import
        ///in
        ///instanceof
        ///int
        ///interface
        ///let
        ///long
        ///native
        ///new
        ///null
        ///package
        ///private
        ///protected
        ///public
        ///return
        ///short
        ///static
        ///super
        ///switch
        ///synchronized
        ///this
        ///throw
        ///throws
        ///transient
        ///true
        ///try
        ///typeof
        ///var
        ///void
        ///volatile
        ///while
        ///with
        ///yield
        ///Array
        ///Date
        ///eval
        ///function [rest of string was truncated]&quot;;.
        /// </summary>
        public static string JavaScriptKeywords {
            get {
                return ResourceManager.GetString("JavaScriptKeywords", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Edit block script....
        /// </summary>
        public static string MainWindowTitle {
            get {
                return ResourceManager.GetString("MainWindowTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Edit block script....
        /// </summary>
        public static string MenuItemText {
            get {
                return ResourceManager.GetString("MenuItemText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to OK.
        /// </summary>
        public static string OkButtonTitle {
            get {
                return ResourceManager.GetString("OkButtonTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Mobiscript editor can only be used in the Blocks, or Applicats tables..
        /// </summary>
        public static string WrongTableMessageText {
            get {
                return ResourceManager.GetString("WrongTableMessageText", resourceCulture);
            }
        }
    }
}
