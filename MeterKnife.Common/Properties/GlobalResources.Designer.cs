﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18444
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MeterKnife.Common.Properties {
    using System;
    
    
    /// <summary>
    ///   一个强类型的资源类，用于查找本地化的字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 VS 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class GlobalResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal GlobalResources() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MeterKnife.Common.Properties.GlobalResources", typeof(GlobalResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   使用此强类型资源类，为所有资源查找
        ///   重写当前线程的 CurrentUICulture 属性。
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
        ///   查找 System.Drawing.Bitmap 类型的本地化资源。
        /// </summary>
        public static System.Drawing.Bitmap care {
            get {
                object obj = ResourceManager.GetObject("care", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   查找类似 &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot; ?&gt;
        ///&lt;MeterParam isScpi=&quot;true&quot; format=&quot;scpi&quot;&gt;
        ///  &lt;command content=&quot;重置&quot; command=&quot;*RST&quot;&gt;&lt;/command&gt;
        ///  &lt;command content=&quot;清屏&quot; command=&quot;*CLS&quot;&gt;&lt;/command&gt;
        ///  &lt;command content=&quot;初始化&quot; command=&quot;INIT&quot;&gt;&lt;/command&gt;
        ///  &lt;command content=&quot;输出&quot; command=&quot;FETC&quot; isReturn=&quot;true&quot;&gt;&lt;/command&gt;
        ///  &lt;command content=&quot;读取&quot; command=&quot;READ&quot; isReturn=&quot;true&quot;&gt;&lt;/command&gt;
        ///  &lt;command content=&quot;配置测量项目并测量&quot; command=&quot;MEAS&quot; isReturn=&quot;true&quot;&gt;
        ///    &lt;command content=&quot;直流电压&quot; command=&quot;VOLT:DC&quot;&gt;&lt;/command&gt;
        ///    &lt;command conten [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        public static string DemoMeterParamElement {
            get {
                return ResourceManager.GetString("DemoMeterParamElement", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似于 (Icon) 的 System.Drawing.Icon 类型的本地化资源。
        /// </summary>
        public static System.Drawing.Icon main_icon {
            get {
                object obj = ResourceManager.GetObject("main_icon", resourceCulture);
                return ((System.Drawing.Icon)(obj));
            }
        }
        
        /// <summary>
        ///   查找 System.Drawing.Bitmap 类型的本地化资源。
        /// </summary>
        public static System.Drawing.Bitmap meter {
            get {
                object obj = ResourceManager.GetObject("meter", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   查找 System.Drawing.Bitmap 类型的本地化资源。
        /// </summary>
        public static System.Drawing.Bitmap pc {
            get {
                object obj = ResourceManager.GetObject("pc", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   查找 System.Drawing.Bitmap 类型的本地化资源。
        /// </summary>
        public static System.Drawing.Bitmap serial {
            get {
                object obj = ResourceManager.GetObject("serial", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
    }
}
