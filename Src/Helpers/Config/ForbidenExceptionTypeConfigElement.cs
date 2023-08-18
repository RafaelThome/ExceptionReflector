using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;

namespace ExceptionReflector.Config
{
    //[DebuggerStepThrough]
    public class ForbidenExceptionTypeConfigElement : ConfigurationElement
    {
        [TypeConverter(typeof(TypeNameConverter))]
        [ConfigurationProperty("Value", IsKey = true, IsRequired = true)]
        public Type Value
        {
            get
            {
                var xTp = this["Value"] as Type;
                if (xTp == null)
                    return null;
                else
                    return xTp;
            }
            set
            {
                var xTp = value as Type;
                if (xTp == null)
                    throw new ConfigurationErrorsException("Não foi possível resolver a string ForbidenExceptionType em um Type.", base.CurrentConfiguration.FilePath, base.ElementInformation.LineNumber);

                if (!xTp.IsAssignableFrom(typeof(Exception)))
                    throw new ConfigurationErrorsException("O Type para o qual foi resolvida a string ForbidenExceptionType não é uma Exception.", base.CurrentConfiguration.FilePath, base.ElementInformation.LineNumber);

                this["Value"] = xTp;
            }
        }

    }
}
