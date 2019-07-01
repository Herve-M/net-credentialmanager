using System.Collections.Generic;

namespace CM.Abstractions
{
    public interface ICredentialManager<TCred, TCredInfo> 
        where TCred: ICredential
        where TCredInfo : ICredentialTargetInformation
    {
        TCred FindBest(string targetName);
        IReadOnlyList<TCred> Enumerate(string filter = null);
        TCred Read(string targetName, CredentialType type);        
        void Write(TCred credential);        
        void Delete(string targetName, CredentialType type);

        // Domain related 
        TCredInfo GetTargetInfo(string targetName);
        IReadOnlyList<TCred> ReadDomainCredential(TCredInfo credentialTargetInformation);
        bool WriteDomainCredential(TCredInfo credentialTargetInformation, TCred credential);

        // Encryption related
        CredentialProtectionType IsProtected(string targetName);
        string Protect(string toEncrypt, bool perThreadSecurityContext, out CredentialProtectionType protectionType); //TODO: API?
        void Unprotect(bool perThreadSecurityContext); //TODO: API?

        bool IsMarshaledCredential(string marshaledCredential);
        string MarshalCredential<Y>(CredentialMarshalType type, in Y credential) where Y : BaseInfoClass; //TOSEE: class, struct or interface?
        BaseInfoClass UnmarshalCredential(string marshaledCredential, out CredentialMarshalType type);
    }
}