# HELP LIFE
![LOGO](logo.png)
## Descrição
A fim de proporcionar um acompanhamento exclusivo para o paciente, criamos o HelpLife. Nosso produto irá monitorar cada beneficiário durante 24 horas para garantir sua proteção e prevenção de sua saúde. Além disso, poderão inicar um chat com um médico a sua escolha ou será disponibilizado um profissional qualificado.

# Configuração da Variável de Ambiente para HelpLife

Este projeto requer a configuração de uma variável de ambiente chamada `GOOGLE_APPLICATION_CREDENTIALS` para autenticação com serviços do Google.

## Configuração da Variável de Ambiente

Para configurar a variável de ambiente `GOOGLE_APPLICATION_CREDENTIALS` com as credenciais necessárias, siga os passos abaixo:

### Passo 1: Credenciais JSON

Obtenha o arquivo JSON com as credenciais de serviço do Google. Aqui está um exemplo de um arquivo `google_credentials.json`:

```json
{
  "type": "service_account",
  "project_id": "chathelplife",
  "private_key_id": "60212d477ed1c172d20d0c956dbacdd8bbc5fac3",
  "private_key": "-----BEGIN PRIVATE KEY-----\nMIIEvAIBADANBgkqhkiG9w0BAQEFAASCBKYwggSiAgEAAoIBAQDN5/8lThAB6hPM\nAM0SomqnHtK7/Q2gV9QgVQHfipIeiCUbQmzvKR76skPxAQzprRgNIoN0cLTN2CU/\nv3Aml1TPRJfa3zOi0PosdSYIzZNt8pepIaSqRU6aAtadj2+v1d2G/BRh9XDIzlDX\na4gEXGfbiyUlxddOLwzfYK0BQeWDayr03HRX5UAGgCnpo3ifBgqa67Gb6jfoqSHi\n5fI7EP28BIQUaXLdfQa6bNlD8d4Ko8fArH/mUCfTVW2iHUUjR8QcsjTcc81dXYwN\nosVNvURn3r5U0JKWGXQMT3veIBFdpLUUFEnpUwdG6mxo9UAhWlSh6Xs8q76r3yWD\nqj1vwjuRAgMBAAECggEAK0r5swrIQ9bHJdAPTjk3Cn9pGJyaxllhIREujSVSFrhS\ncPpU9C4ltNlRfrVVVTaPRYe5svxVj6hAILisKHkOHGxUL3FZpoSg7UMOZbBvrKDi\nsW2A9UoQPkKVhFhZx9NEOjL7SCN2fMogZkLWPPB+58LIYzpHTEmJP9hQV+DWlHxN\ngXUYnOItRsV2T4xvSAqh+F5PDcs6WSFPDmU8AwilYrp4XRiUHDV6blQar9Ea53oz\n8BO9peZ5lQQ6NJpSJusjG6toC5f6Bv5st/EVKcSFLT3AQBF1py2hEI/bkUbeH7p0\nndLy6bwDvwUYtdTAVFayvhjf6jVvLlk5ZKuaB96hyQKBgQD7TB9voJU1RVIZ90Ql\n/vxBv9YKMivxpqM5QfgBfW4TBVKtsoJF9gRwBI5pDNlE+uDYbsw+JUnUuIr9RZeE\nN589Xf7VBqy1g+N2V7uBO2MgowNXAgYaigYV/qceei/r+ebyh0v4eMa1yscCUfXl\nKXR0sz02Hl1VUYZq6J2QvQBebQKBgQDRwmvGLCo5DYEpXvoHEUsT8Kh62d4T8O9h\nB+waVSiD22rbc/13LywlRley4o8t4baN0MpeDjJfyNYHFMqEOuFUPkvceh8DSKtG\nuQAYCNiMM8CdI402S8HEUs7BJ0pTu0DCM3dLLOMgOU5FNDQMvh5s9tOeYFrjkK4/\n71lHUSALNQKBgFnl+JUBeH9PJjjdyUZierG2xAZvvmK7f0bbf7JgwcESNWqoIoyO\nnzXHm36fNxbDPS6QLIbK5iewQSCsBbEqCNB2UNS6h4yD+xp59j0cFrE1P9ufgLl7\nyQOTTwvLIFul0eXPWvEvm76rJRiCY5owg1XATVSP9kmwWYrjXoLJ6ljdAoGAPBAd\n6DsRRBD34S7PDWMISodo1vS3wKIHCZPJKakk1sznhjtqYsxhKbNAvXpeYVb2fRlC\nOTh7eC38Cz8K1l4902+bLWDSrJAMa03iFv/BcjQHfXeGofuDWoITrf9gKyW9X155\nVE9gIgQGw441MUjnUXBEHF+e5vawzqHswJ0cPdkCgYASUmcMpR3KB65F6kpp9SP5\nEcNTQlspN4L8sPwzN8Z2imR5lfRSdkXjQI5GW6qmaIBcaTadWjOi54+FoNSLTQWY\nyYEicv6d269d9PTcnHL0x9Jig0nY1INZeMPUrGAAoYP8DzNZaQaKz961RDdy3+Il\nOrJnf5/RdSLcLxJul/yojw==\n-----END PRIVATE KEY-----\n",
  "client_email": "firebase-adminsdk-70wgo@chathelplife.iam.gserviceaccount.com",
  "client_id": "111168437444422986855",
  "auth_uri": "https://accounts.google.com/o/oauth2/auth",
  "token_uri": "https://oauth2.googleapis.com/token",
  "auth_provider_x509_cert_url": "https://www.googleapis.com/oauth2/v1/certs",
  "client_x509_cert_url": "https://www.googleapis.com/robot/v1/metadata/x509/firebase-adminsdk-70wgo%40chathelplife.iam.gserviceaccount.com",
  "universe_domain": "googleapis.com"
}
