# Problem Statement
When bootstrapping a linux docker image with a self-signed certificate and corresponding private key,
dotnetcore 2.0 applications were able to access the certificate but not the private key.

The same code running in windows was able to retrieve both certificate and private key.

## Test Methods
### Ubuntu 17.10 - dotNetCore 2.1.4
`server.crt` placed in `/etc/ssl/certs`

`server.key` (unencrypted, no passphrase) placed in `/etc/ssl/private`

Result: 
![Ubuntu Result](https://i.imgur.com/FwnXf7g.png)


### Windows 10 v1709 - dotNetCore 2.1.4
`server.pfx` imported to `Trusted Root Certification Authorities\Certificates` (blank passphrase)

Result:
![Windows Result](https://i.imgur.com/507eNVW.png)
