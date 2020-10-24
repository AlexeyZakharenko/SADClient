using RestSharp;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SADClient
{
    public class Auth0RestClient: RestClient
    {
        private const string token = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IkhHZjJFUjM2dG5YOC1NSTVSNDJ4SiJ9.eyJpc3MiOiJodHRwczovL2Rldi0wdGRqYTd1aC5ldS5hdXRoMC5jb20vIiwic3ViIjoiS3FPOVRpZG05VUFTUnVyaXRJS1lyWEphQU8wczdscUdAY2xpZW50cyIsImF1ZCI6Imh0dHBzOi8vU0FEQXNzZXNzbWVudC5jb20iLCJpYXQiOjE2MDM1NDk0MzgsImV4cCI6MTYwMzYzNTgzOCwiYXpwIjoiS3FPOVRpZG05VUFTUnVyaXRJS1lyWEphQU8wczdscUciLCJndHkiOiJjbGllbnQtY3JlZGVudGlhbHMifQ.fY2U5OtJ7Vo8J7n_I_Vn7wkkTu_SXjIm-nylSe4P0F6N22ZQH-_HxRtpfinm0danCx9Wi-08vS5PgzLbFNYWJ7Ev_KUaP3lPaJfqAEOu4yptOnTQafH6Spy9q-QhQKD0iFOmbZ3v4PJnMLrBfSZR0ELmSXAe0VBjJy3VZGjCIXkown4pLecbfRRFaObUk88RcFrFqHJGExFRMj1blrb9Y91SFblDCVHW9V_5eew5ksy0spUvsuSSa2eXcEju9aySffQR1LblzmSvkzpM0eUqVmZDDdHp9emIsW0UUKHRQgheKpgweds6eCN3GM6i5yODcBTc8D9IJs3-kC3-bpK3oA";

        public Auth0RestClient() : base() { }
        public Auth0RestClient(string url) : base(url) { }
        public Auth0RestClient(Uri uri) : base(uri) { }


        // Override base Execute method to pass authorization token to the request
        public override IRestResponse Execute(IRestRequest request)
        {
            request.AddHeader("authorization", $"Bearer {token}");
            return base.Execute(request);
        }
    }
}
