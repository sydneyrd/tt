const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:21790';

const PROXY_CONFIG = [
  {
    context: [
      "/weatherforecast",
   ],
    target: target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  },
// i need to add chathub here, i'll try this way first, since the sample weather forecast is working
// but it may be different since it's a signalr hub

  {
    context: [
      "/chathub",
      "/chatHub/**"
    ],
    target: target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  }

]
//chatty's suggestion
// module.exports = PROXY_CONFIG;
// {
//   "/chathub": {
//     "target": "http://localhost:44478",
//     "secure": false,
//     "changeOrigin": true,
//     "logLevel": "debug"
//   }
// }