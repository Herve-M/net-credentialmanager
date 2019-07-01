using System;
using System.Collections.Generic;

namespace CM.Abstractions
{
    /// <summary>
    /// <para>The <c>CREDENTIAL_TARGET_INFORMATION</c> structure contains the target computer's name, domain, and tree.</para>
    /// </summary>
    public interface ICredentialTargetInformation
    {
        /// <summary>
        /// Array specifying the credential types acceptable by the authentication package used by the target server. Each element is one
        /// of the CRED_TYPE_* defines. The order of this array specifies the preference order of the authentication package. More
        /// preferable types are specified earlier in the list.
        /// </summary>
        ICollection<CredentialType> CredentialTypes { get; set; }

        /// <summary>Number of elements in the CredTypes array.</summary>
        uint CredTypeCount { get; }

        /// <summary>
        /// Attributes of the target.
        /// </summary>
        TargetAttribute Flags { get; set; }

        /// <summary>
        /// DNS name of the target server's domain. If the name is not known, this member can be NULL. If the target server is a member
        /// of a workgroup, this member must be NULL.
        /// </summary>
        string DnsDomainName { get; set; }

        /// <summary>DNS name of the target server. If the name is not known, this member can be NULL.</summary>
        string DnsServerName { get; set; }

        /// <summary>
        /// DNS name of the target server's tree. If the tree name is not known, this member can be NULL. If the target server is a
        /// member of a workgroup, this member must be NULL.
        /// </summary>
        string DnsTreeName { get; set; }

        /// <summary>
        /// NetBIOS name of the target server's domain. If the name is not known, this member can be NULL. If the target server is a
        /// member of a workgroup, this member must be NULL.
        /// </summary>
        string NetbiosDomainName { get; set; }

        /// <summary>NetBIOS name of the target server. If the name is not known, this member can be NULL.</summary>
        string NetbiosServerName { get; set; }

        /// <summary>
        /// Name of the authentication package that determined the values NetbiosServerName, DnsServerName, NetbiosDomainName,
        /// DnsDomainName, and DnsTreeName as a function of TargetName. This member can be passed to AcquireCredentialsHandle as the
        /// package name.
        /// </summary>
        string PackageName { get; set; }

        /// <summary>
        /// Name of the target server as specified by the caller accessing the target. It is typically the NetBIOS or DNS name of the
        /// target server.
        /// </summary>
        string TargetName { get; set; }        
    }

    [Flags]
    public enum TargetAttribute : uint
    {
        None = (1 << 0),

        /// <summary>
        /// <description>
        /// Set if the authentication package cannot determine whether the server name is a DNS name or a NetBIOS name. In that case, the
        /// NetbiosServerName member is set to NULL and the DnsServerName member is set to the server name of unknown format.
        /// </description>
        /// </summary>
        ServeFormatUnknown = (1 << 1),

        /// <summary>
        /// <description>
        /// Set if the authentication package cannot determine whether the domain name is a DNS name or a NetBIOS name. In that case, the
        /// NetbiosDomainName member is set to NULL and the DnsDomainName member is set to the domain name of unknown format.
        /// </description>
        /// </summary>
        DomainFormatUnknown = (1 << 2),

        /// <summary>
        /// <description>
        /// Set if the authentication package has determined that the server only needs a password to authenticate. The caller can use
        /// this flag to prompt only for a password and not a user name.
        /// </description> 
        /// </summary>
        OnlyPsswordRequired = (1 << 3)
    }
}