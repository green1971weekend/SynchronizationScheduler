﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SynchronizationScheduler.Application.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class PostSynchronizationService {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal PostSynchronizationService() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SynchronizationScheduler.Application.Resources.PostSynchronizationService", typeof(PostSynchronizationService).Assembly);
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
        ///   Looks up a localized string similar to Synchronization for adding posts to the local database has ended..
        /// </summary>
        public static string EndSynchronizationForAdditionPost {
            get {
                return ResourceManager.GetString("EndSynchronizationForAdditionPost", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Synchronization for deleting posts to the local database has ended..
        /// </summary>
        public static string EndSynchronizationForDeletionPost {
            get {
                return ResourceManager.GetString("EndSynchronizationForDeletionPost", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Synchronization for updating posts to the local database has ended..
        /// </summary>
        public static string EndSynchronizationForUpdationPost {
            get {
                return ResourceManager.GetString("EndSynchronizationForUpdationPost", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Synchronization for adding posts to the local database has started..
        /// </summary>
        public static string StartSynchronizationForAdditionPost {
            get {
                return ResourceManager.GetString("StartSynchronizationForAdditionPost", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Synchronization for deleting posts to the local database has started..
        /// </summary>
        public static string StartSynchronizationForDeletionPost {
            get {
                return ResourceManager.GetString("StartSynchronizationForDeletionPost", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Synchronization for updating posts to the local database has started..
        /// </summary>
        public static string StartSynchronizationForUpdationPost {
            get {
                return ResourceManager.GetString("StartSynchronizationForUpdationPost", resourceCulture);
            }
        }
    }
}
