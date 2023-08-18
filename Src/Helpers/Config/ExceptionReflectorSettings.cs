using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;

namespace ExceptionReflector.Config
{
    //[DebuggerStepThrough]
    public static class ExceptionReflectorSettings
    {
        public static ExceptionReflectorConfigurationSection ExceptionReflectorConfigSection
        {
            get
            {
                return (ExceptionReflectorConfigurationSection)ConfigurationManager.GetSection("ExceptionReflectorSection");
            }
        }

        public static ForbidenExceptionTypesConfigElemCollection ForbidenExceptionTypesElement
        {
            get
            {
                return ExceptionReflectorConfigSection.ForbidenExceptionTypes;
            }
        }

        public static bool BypassOpCodeExceptions
        {
            get
            {
                return ExceptionReflectorConfigSection.BypassOpCodeExceptions;
            }
        }

        public static IEnumerable<Type> ForbidenExceptionTypes
        {
            get
            {
                foreach (ForbidenExceptionTypeConfigElement element in ForbidenExceptionTypesElement)
                {
                    if (element != null)
                        yield return element.Value;
                }
            }
        }
    }
}
