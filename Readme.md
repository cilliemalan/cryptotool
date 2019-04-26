# Crypto Tool
A small program for generating CSRs in bulk. Uses BouncyCastle crypto library for crypto magic.
![it's a tool](https://i.imgur.com/alJdreX.png)

## Building
Requires Visual Studio 2017 or later. Just open and build.

## Using

### Inspect Tab
The application works in the context of a "workspace" (this is the top URL like bar). It will scan this directory for all certificate like things. Each recognized file will be shown in the box on the left, with details on the right. If a file is selected, the box will also highlight other files with the same keys or a corresponding public/private key.

### Generate Tab
On the Generate tab, CSRs and self-signed certificates can be generated (in bulk if you want). The box to the left takes a list of names. For server certificates this is the DNS name. It will generate a CSR or certificate for each line. You can put multiple items on one line, seperated by commas. If you put multiple items on a line, they will be populated as the subject alternative name. The first item will be the common name (CN).

For more information about the options, check the OpenSSL docs or just google it. Or update this readme and make a PR.

### Sign Certificates Tab
On the sign certificates tab you can sign certificates. For more info see [HOWTO_CA.md](HOWTO_CA.md#how-to-use-crypto-tool-as-a-certificate-authority).
![it's a SIGNING tool](https://i.imgur.com/9PDUfog.png)
On the left select a CA certificate. It will show all detected CA certificates with keys. If you don't see your certificate that's because it doesn't have the right basic constraint or the key is not available. You can generate a CA certificate on the Generate tab by selecting Certificate Authority as a basic constraint and selecting Certificate Key Enchipherment as a key usage.

In the middle, check the CSRs you want to sign.

On the right, select the duration and click sign. It will put certificates right next to the CSRs with a different extension.

## Stuff not done (yet)
- It doesn't do fancy stuff like OCSP, CRL, SCT, etc etc etc. It only supports the most common extensions.
- It doesn't show certificate chains.
- It doesn't show CSR details in the info panel.
- It can under some circumstances generate invalid certificates (i.e. if you specify invalid usages).
- It doesn't support PKCS12 containers (p12/pfx files). To combine `.key` and `.cer` files
  into a PKCS12 file using openssl, [see this](https://www.ssl.com/how-to/create-a-pfx-p12-certificate-file-using-openssl/).
- It does not securely save keys (i.e. with passwords). It just plops them on disk.
- It only saves files as PEM.
- You cannot modify extensions when signing certificates.
- Self signed certificates have a hardcoded duration of 30 years.

## License
see [LICENSE](LICENSE)
