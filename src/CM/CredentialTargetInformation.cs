using CM.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using Vanara.InteropServices;
using Vanara.PInvoke;

namespace CM
{
    /// <summary>
    /// <para>The <c>CREDENTIAL_TARGET_INFORMATION</c> structure contains the target computer's name, domain, and tree.</para>
    /// </summary>
    public class CredentialTargetInformation : ICredentialTargetInformation
    {
        /// <summary>
        /// Name of the target server as specified by the caller accessing the target. It is typically the NetBIOS or DNS name of the
        /// target server.
        /// </summary>
        public string TargetName { get; set; }

        /// <summary>NetBIOS name of the target server. If the name is not known, this member can be NULL.</summary>
        public string NetbiosServerName { get; set; }

        /// <summary>DNS name of the target server. If the name is not known, this member can be NULL.</summary>
        public string DnsServerName { get; set; }

        /// <summary>
        /// NetBIOS name of the target server's domain. If the name is not known, this member can be NULL. If the target server is a
        /// member of a workgroup, this member must be NULL.
        /// </summary>
        public string NetbiosDomainName { get; set; }

        /// <summary>
        /// DNS name of the target server's domain. If the name is not known, this member can be NULL. If the target server is a member
        /// of a workgroup, this member must be NULL.
        /// </summary>
        public string DnsDomainName { get; set; }

        /// <summary>
        /// DNS name of the target server's tree. If the tree name is not known, this member can be NULL. If the target server is a
        /// member of a workgroup, this member must be NULL.
        /// </summary>
        public string DnsTreeName { get; set; }

        /// <summary>
        /// Name of the authentication package that determined the values NetbiosServerName, DnsServerName, NetbiosDomainName,
        /// DnsDomainName, and DnsTreeName as a function of TargetName. This member can be passed to AcquireCredentialsHandle as the
        /// package name.
        /// </summary>
        public string PackageName { get; set; }

        /// <summary>
        /// Attributes of the target.
        /// </summary>
        public TargetAttribute Flags { get; set; }

        /// <summary>Number of elements in the CredTypes array.</summary>
        public uint CredTypeCount => (uint)(CredentialTypes?.Count ?? default);

        public ICollection<CredentialType> CredentialTypes { get; set; }

        public static implicit operator AdvApi32.CREDENTIAL_TARGET_INFORMATION(CredentialTargetInformation cti)
        {
            var uCredTypes = cti.CredTypeCount == 0 ?
                IntPtr.Zero :
                SafeHGlobalHandle.CreateFromList<AdvApi32.CRED_TYPE>(cti.CredentialTypes.Select(x => (AdvApi32.CRED_TYPE)x));

            return new AdvApi32.CREDENTIAL_TARGET_INFORMATION()
            {
                TargetName = cti.TargetName,
                NetbiosServerName = cti.NetbiosServerName,
                DnsServerName = cti.DnsServerName,
                NetbiosDomainName = cti.NetbiosDomainName,
                DnsDomainName = cti.DnsDomainName,
                DnsTreeName = cti.DnsTreeName,
                PackageName = cti.PackageName,
                Flags = (uint)cti.Flags,
                CredTypeCount = cti.CredTypeCount,
                CredTypes = uCredTypes.DangerousGetHandle(),
            };
        }
    }    
}
