# Crypto Tool
A small program for generating CSRs in bulk. Uses BouncyCastle crypto library for crypto magic.
![it's a tool](https://i.imgur.com/IbmrSjR.png)

## Building
Requires Visual Studio 2017 or later. Just open and build.

## Using

### Inspect Tab
The application works in the context of a "workspace" (this is the top URL like bar). It will scan this directory for all certificate like things. Each recognized file will be shown in the box on the left, with details on the right. If a file is selected, the box will also highlight other files with the same keys or a corresponding public/private key.

### CSR tab
On the CSR tab, CSRs can be generated in bulk. The box to the left takes a list of DNS names. It will generate a CSR for each line. You can put multiple items on one line, seperated by commas. If you put multiple items on a line, they will be populated as the subject alternative name. The first item will be the common name (CN).

For more information about the options, check the OpenSSL docs or just google it. Or update this readme and make a PR.

## License
see [LICENSE](LICENSE)
