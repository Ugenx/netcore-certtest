using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace netcore.certtest
{
    class Program
    {
        private const string DefaultThumbprint = "BC6B3F7414BE8F5C2632C3BCE199B6DC33092EE5";
        private const StoreName _StoreName = StoreName.Root;
        private const StoreLocation _StoreLocation = StoreLocation.LocalMachine;

        public static void Main(string[] args)
        {
            var certificateThumbprint = DefaultThumbprint;

            if (args?.Length > 0 && !string.IsNullOrEmpty(args[0]))            
                certificateThumbprint = args[0];
                
            var certificate = FindCertificateWithPrivateKeyByThumbprint(certificateThumbprint);

            Console.WriteLine($"Found certificate with thumbprint [{certificateThumbprint}] in the [{_StoreLocation}]:[{_StoreName}] store. " +
                              $"Has private key: [{certificate.HasPrivateKey}]");
        }

        /// <summary>
        /// Helper function to load an X509 certificate
        /// </summary>
        /// <param name="certificateThumbprint">Thumbprint of the certificate to be loaded</param>
        /// <param name="validOnly">Flag indicating whether to check if certificate is valid</param>
        /// <returns>X509 Certificate</returns>
        private static X509Certificate2 FindCertificateWithPrivateKeyByThumbprint(string certificateThumbprint, bool validOnly = false)
        {
            if (certificateThumbprint == null)
                throw new ArgumentNullException(nameof(certificateThumbprint));            

            var store = new X509Store(_StoreName, _StoreLocation);
            store.Open(OpenFlags.ReadOnly);

            var certificateCollection = store.Certificates.Find(
                X509FindType.FindByThumbprint,
                certificateThumbprint,
                validOnly);

            if (certificateCollection == null || certificateCollection.Count == 0)
                throw new Exception($"Could not find a certificate with thumbprint [{certificateThumbprint}] " +
                                    $"in the [{_StoreLocation}]:[{_StoreName}] store.");

            var enumerator = certificateCollection.GetEnumerator();
            enumerator.MoveNext();
            var certificate = enumerator.Current;
            store.Close();

            return certificate;
        }
    }
}
