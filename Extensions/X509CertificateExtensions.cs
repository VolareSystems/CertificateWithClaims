﻿using System;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace CertificateWithClaims.Extensions
{
    public static class X509Certificate2Extensions
    {
        public static bool IsSelfSigned(this X509Certificate2 certificate)
        {
            if (certificate == null)
            {
                throw new ArgumentNullException(nameof(certificate));
            }

            return certificate.SubjectName.RawData.SequenceEqual(certificate.IssuerName.RawData);
        }

        public static string SHA256Thumprint(this X509Certificate2 certificate)
        {
            if (certificate == null)
            {
                throw new ArgumentNullException(nameof(certificate));
            }

            using var hasher = SHA256.Create();
            var certificateHash = hasher.ComputeHash(certificate.RawData);
            var hashAsString = string.Empty;
            foreach (var hashByte in certificateHash)
            {
                hashAsString += hashByte.ToString("x2", CultureInfo.InvariantCulture);
            }

            return hashAsString;
        }
    }
}