using System;
using System.Collections.Generic;
using System.Linq;
using CM.Abstractions;
using Microsoft.Extensions.Logging;
using Vanara.Extensions;
using Vanara.PInvoke;

namespace CM
{
    public sealed class CredentialManager : ICredentialManager<Credential, CredentialTargetInformation>
    {
        private readonly ILogger<CredentialManager> _logger;

        public CredentialManager(ILogger<CredentialManager> logger)
        {
            _logger = logger;
        }

        public Credential FindBest(string targetName)
        {
            if(string.IsNullOrEmpty(targetName) || string.IsNullOrWhiteSpace(targetName))
                throw new ArgumentException($"{nameof(targetName)} must be a valid string", nameof(targetName)); //TODO Impr.

            try
            {
                var success = AdvApi32.CredFindBestCredential(
                    targetName,
                    AdvApi32.CRED_TYPE.CRED_TYPE_GENERIC,
                    default,
                    out var credMemoryHandle
                    );

                _logger?.LogDebug("[{0}]AdvApi32.CredFindBestCredential with target: {1}", success, targetName);

                using (credMemoryHandle)
                {
                    if (credMemoryHandle.IsInvalid)
                        Win32Error.GetLastError().ThrowIfFailed();

                    return new Credential(credMemoryHandle.DangerousGetHandle().ToStructure<AdvApi32.CREDENTIAL>());
                }
            }
            catch (Exception e)
            {
                _logger?.LogError(e, "during CredEnumerate, see:");
                _logger?.LogDebug("Win32 error: {LastError}", Win32Error.GetLastError());
                throw;
            }
        }

        public IReadOnlyList<Credential> Enumerate(string filter = null)
        {
            try
            {
                var success = AdvApi32.CredEnumerate(
                    filter,
                    filter is null ? AdvApi32.CRED_ENUM.CRED_ENUMERATE_ALL_CREDENTIALS : AdvApi32.CRED_ENUM.NONE,
                    out var count,
                    out var credMemoryHandle);

                _logger?.LogDebug("[{0}]AdvApi32.CredEnumerate with filter: {1}, giving {2} results", success, filter, count);

                using (credMemoryHandle)
                {
                    if (credMemoryHandle.IsInvalid)
                        Win32Error.GetLastError().ThrowIfFailed();

                    return credMemoryHandle.DangerousGetHandle()
                        .ToIEnum<IntPtr>((int)count)
                        .Select(p => new Credential(p.ToStructure<AdvApi32.CREDENTIAL>()))
                        .ToList().AsReadOnly();
                }
            }
            catch (Exception e)
            {
                _logger?.LogError(e, "during CredEnumerate, see:");
                _logger?.LogDebug("Win32 error: {LastError}", Win32Error.GetLastError());
                throw;
            }
        }

        public Credential Read(string targetName, CredentialType type)
        {
            if (string.IsNullOrEmpty(targetName))
                throw new ArgumentNullException(nameof(targetName));

            try
            {
                var success = AdvApi32.CredRead(targetName, (AdvApi32.CRED_TYPE)type, 0, out var credMemoryHandle);

                _logger?.LogDebug("[{0}]AdvApi32.CredRead with targetName: {1}", success, targetName);

                using (credMemoryHandle)
                {
                    if (credMemoryHandle.IsInvalid)
                        Win32Error.GetLastError().ThrowIfFailed();

                    return new Credential(credMemoryHandle.DangerousGetHandle().ToStructure<AdvApi32.CREDENTIAL>());
                }
            }
            catch (Exception e)
            {
                _logger?.LogError(e, "during Read of credential, see:");
                _logger?.LogDebug("Win32 error: {LastError}", Win32Error.GetLastError());
                throw;
            }
        }

        // TOSEE: CRED_PRESERVE_CREDENTIAL_BLOB?  `The CredentialBlobSize of the passed in Credential structure must be zero.`
        public void Write(Credential credential)
        {
            try
            {
                var success = AdvApi32.CredWrite(credential, 0);

                _logger?.LogDebug("[{0}]AdvApi32.CredWrite with targetName: {1} and CRED_TYPE: {2}", credential.TargetName, credential.Type);

                //return success;
            }
            catch (Exception e)
            {
                _logger?.LogError(e, "during Write of credential, see:");
                _logger?.LogDebug("Win32 error: {LastError}", Win32Error.GetLastError());
                throw;
            }
        }

        public bool WriteDomainCredential(CredentialTargetInformation credentialTargetInformation, Credential credential)
        {
            try
            {
                var success = AdvApi32.CredWriteDomainCredentials(credentialTargetInformation, credential, 0);

                _logger?.LogDebug("[{0}]AdvApi32.CredWrite with targetName: {1} and CRED_TYPE: {2}", credential.TargetName, credential.Type);

                return success;
            }
            catch (Exception e)
            {
                _logger?.LogError(e, "during Write of credential, see:");
                _logger?.LogDebug("Win32 error: {LastError}", Win32Error.GetLastError());
                throw;
            }
        }

        public void Delete(string targetName, CredentialType type)
        {
            if (string.IsNullOrEmpty(targetName))
                throw new ArgumentNullException(nameof(targetName));

            try
            {
                var success = AdvApi32.CredDelete(targetName, (AdvApi32.CRED_TYPE)type);

                _logger?.LogDebug("[{0}]AdvApi32.CredDelete with targetName: {1} and CRED_TYPE: {2}", success, targetName, type.ToString());

                //return success;
            }
            catch (Exception e)
            {
                _logger?.LogError(e, "during Delete of credential, see:");
                _logger?.LogDebug("Win32 error: {LastError}", Win32Error.GetLastError());
                throw;
            }
        }

        public CredentialTargetInformation GetTargetInfo(string targetName)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<Credential> ReadDomainCredential(CredentialTargetInformation credentialTargetInformation)
        {
            throw new NotImplementedException();
        }

        public CredentialProtectionType IsProtected(string targetName)
        {
            throw new NotImplementedException();
        }

        public string Protect(string toEncrypt, bool perThreadSecurityContext, out CredentialProtectionType protectionType)
        {
            throw new NotImplementedException();
        }

        public void Unprotect(bool perThreadSecurityContext)
        {
            throw new NotImplementedException();
        }

        public bool IsMarshaledCredential(string marshaledCredential)
        {
            throw new NotImplementedException();
        }

        public string MarshalCredential<T>(CredentialMarshalType type, in T credential) where T : BaseInfoClass
        {
            throw new NotImplementedException();
        }

        public BaseInfoClass UnmarshalCredential(string marshaledCredential, out CredentialMarshalType type)
        {
            throw new NotImplementedException();
        }
    }
}
