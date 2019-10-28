# secure-env-poc
驗證前後端加解密套件用測試專案。
<!-- descriptions, main goal to deal with -->
## Getting Started
## Enviorment
.Net Core 2.1  
[https://dotnet.microsoft.com/download/dotnet-core/2.1](https://dotnet.microsoft.com/download/dotnet-core/2.1)

Build based on [aspnetcore-Vue-starter](https://github.com/TrilonIO/aspnetcore-Vue-starter)
### Running the tests
Windows環境下可使用已封裝好的exe執行或是使用以下指令:
```bash
# Installation
$ npm install

# Compile
$ dotnet build

# 啟動(Start)
$ dotnet run
or
$ npm run
```
啟動後即可瀏覽 [http://localhost:5000](http://localhost:5000)進入測試頁面。

PS: 如果遇到localhost憑證不相容的情形, 可以執行以下指令:
```bash
$ dotnet dotnet dev-certs https --trust
```
Ref: [MSDN - 信任.NET Core HTTPS 開發憑證](https://docs.microsoft.com/zh-tw/aspnet/core/security/enforcing-ssl?view=aspnetcore-3.0&tabs=visual-studio#trust-the-aspnet-core-https-development-certificate-on-windows-and-macos)
### Usage

後端API範例測試:
1. 直接使用測試頁面操作。
2. 內有提供Swagger介面供直接操作。  

## API

```JavaScript
// Init with RSA-1024 public key API
const connection = await Connection.getInstance(url);

// Base get/delete
const userData = await connection.get(url);

// post/put with partial encryption
let postBody = { Account: 'test' ,Password: 'test' };
let postOption = { encrypt: ['Password'] }; //option style same as axios
const response = await connection.post(url, postBody, postOption);

//ppost/put with fully encryption
let postBody = { Account: 'test' ,Password: 'test' };
let postOption = { encrypt: 'all' };  //option style same as axios
const response = await connection.post(url, postBody, postOption);
```
### Build with

npm
.Net Core 2.1  
<!-- how to install or build your project -->
### References

[axios](https://github.com/axios/axios) - 0.18.0  
[cryptico-js](https://github.com/wwwtyro/cryptico) - 1.1.0

<!-- limitation of this POC -->
### License
