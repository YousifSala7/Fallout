using System;
using System.Linq;
using System.Reflection;

namespace Fallout.Common.Tools.AzureKeyVault
{
    /// <summary>Attribute to obtain a secret from the Azure KeyVault defined by <see cref="AzureKeyVaultConfigurationAttribute"/>.</summary>
    public class AzureKeyVaultSecretAttribute : AzureKeyVaultAttributeBase
    {
        private readonly string secretName;

        public AzureKeyVaultSecretAttribute(string secretName = null)
        {
            this.secretName = secretName;
        }

        protected override object GetValue(AzureKeyVaultConfiguration configuration, MemberInfo member)
        {
            return ParameterService.GetParameter<string>(member.Name) ??
                   AzureKeyVaultTasks.GetSecret(configuration, secretName ?? member.Name);
        }
    }
}
