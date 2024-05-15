using GD.Protocol;
using Donatie = GestiuneDonatii.model.Donatie;
using proto = GD.Protocol;

namespace Protobuf
{
    public class ProtoUtils
    {
        public static GDRequest CreateLoginRequest(Voluntar user)
        {
            proto.Voluntar voluntarProto = new proto.Voluntar
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.Password
            };
            GDRequest request = new GDRequest { Type = GDRequest.Types.Type.Login, User = voluntarProto };
            return request;
        }

        public static GDResponse CreateErrorResponse(string errorMessage)
        {
            GDResponse resp = new GDResponse
            {
                Type = GDResponse.Types.Type.Error,
                Error = errorMessage
            };
            return resp;
        }

        public static GDResponse CreateOkResponse()
        {
            GDResponse response = new GDResponse { Type = GDResponse.Types.Type.Ok };
            return response;
        }

        public static GDRequest CreateLogoutRequest(Voluntar voluntar)
        {
            proto.Voluntar voluntarProto = new proto.Voluntar
            {
                Id = voluntar.Id,
                Username = voluntar.Username,
                Password = voluntar.Password
            };
            GDRequest request = new GDRequest { Type = GDRequest.Types.Type.Logout, User = voluntarProto };
            return request;
        }

        public static GDRequest CreateAddDonatorRequest(Donator donator)
        {
            proto.Donator donatorProto = new proto.Donator
            {
                Id = donator.Id,
                Nume = donator.Nume,
                Adresa = donator.Adresa,
                Telefon = donator.Telefon
            };
            GDRequest request = new GDRequest { Type = GDRequest.Types.Type.NewDonator, Donator = donatorProto };
            return request;
        }


        public static GDRequest CreateFindDonatorRequest(Donator donator)
        {
            proto.Donator donatorProto = new proto.Donator
            {
                Id = donator.Id,
                Nume = donator.Nume,
                Adresa = donator.Adresa,
                Telefon = donator.Telefon
            };
            GDRequest request = new GDRequest { Type = GDRequest.Types.Type.FindDonator, Donator = donatorProto };
            return request;
        }

        public static GDRequest CreateFindCauzaRequest(Cauza cauza)
        {
            proto.Cauza cauzaProto = new proto.Cauza
            {
                Id = cauza.Id,
                Nume = cauza.Nume,
                Descriere = cauza.Descriere
            };
            GDRequest request = new GDRequest { Type = GDRequest.Types.Type.FindCauza, Cauza = cauzaProto };
            return request;
        }


        public static GestiuneDonatii.model.Voluntar GetVoluntar(GDRequest request)
        {
            GestiuneDonatii.model.Voluntar voluntar =
                new GestiuneDonatii.model.Voluntar(long.Parse(request.User.Id), request.User.Username,
                    request.User.Password);
            return voluntar;
        }

        public static GestiuneDonatii.model.Cauza GetCauza(GDRequest request)
        {
            GestiuneDonatii.model.Cauza cauza = new GestiuneDonatii.model.Cauza(long.Parse(request.Cauza.Id),
                request.Cauza.Nume,
                request.Cauza.Descriere);
            return cauza;
        }

        public static GDResponse? CreateFindCauzaResponse(GestiuneDonatii.model.Cauza cauza)
        {
            GDResponse response = new GDResponse
            {
                Type = GDResponse.Types.Type.FindCauza
            };

            proto.Cauza c = new proto.Cauza
            {
                Id = cauza.Id.ToString(),
                Nume = cauza.Nume,
                Descriere = cauza.Descriere
            };

            response.Cauze.Add(c);
            return response;
        }

        public static Donatie GetDonatie(GDRequest request)
        {
            GestiuneDonatii.model.Donatie donatie = new GestiuneDonatii.model.Donatie(long.Parse(request.Donatie.Id),
                GetDonator(request), GetCauza(request), float.Parse(request.Donatie.Suma));
            return donatie;
        }

        public static GestiuneDonatii.model.Donator GetDonator(GDRequest request)
        {
            GestiuneDonatii.model.Donator donator = new GestiuneDonatii.model.Donator(long.Parse(request.Donator.Id),
                request.Donator.Nume, long.Parse(request.Donator.Telefon), request.Donator.Adresa);
            return donator;
        }

        public static GDResponse? CreateFindDonatorResponse(GestiuneDonatii.model.Donator? donator)
        {
            GDResponse response = new GDResponse
            {
                Type = GDResponse.Types.Type.FindDonator
            };

            proto.Donator d = new proto.Donator
            {
                Id = donator.Id.ToString(),
                Nume = donator.Nume,
                Adresa = donator.Adresa,
                Telefon = donator.Telefon.ToString()
            };

            response.Donator = d;
            return response;
        }

        public static GDResponse? CreateGetDonatorsResponse(List<GestiuneDonatii.model.Donator> donatori)
        {
            GDResponse response = new GDResponse
            {
                Type = GDResponse.Types.Type.GetDonators
            };

            foreach (var donator in donatori)
            {
                proto.Donator d = new proto.Donator
                {
                    Id = donator.Id.ToString(),
                    Nume = donator.Nume,
                    Adresa = donator.Adresa,
                    Telefon = donator.Telefon.ToString()
                };
                response.Donators.Add(d);
            }

            return response;
        }

        public static GDResponse? CreateGetDonationsResponse(Dictionary<string, float> donations)
        {
            GDResponse response = new GDResponse
            {
                Type = GDResponse.Types.Type.GetDonations
            };

            foreach (var donation in donations)
            {
                proto.Donatie d = new proto.Donatie
                {
                    CauzaId = donation.Key,
                    Suma = donation.Value.ToString()
                };
                response.Donations.Add(d);
            }

            return response;
        }

        public static GDResponse CreateNewDonationResponse(Donatie donatie)
        {
            proto.Donatie d = new proto.Donatie
            {
                CauzaId = donatie.Cauza.Id.ToString(),
                DonatorId = donatie.Donator?.Id.ToString(),
            };

            GDResponse response = new GDResponse
            {
                Type = GDResponse.Types.Type.NewDonation,
                Donatie = d
            };

            return response;
        }

        public static GDResponse CreateNewDonatorResponse(GestiuneDonatii.model.Donator? donator)
        {
            proto.Donator donatorDTO = new proto.Donator
            {
                Id = donator.Id.ToString(),
                Nume = donator.Nume,
                Adresa = donator.Adresa,
                Telefon = donator.Telefon.ToString()
            };

            GDResponse response = new GDResponse
            {
                Type = GDResponse.Types.Type.NewDonator,
                Donator = donatorDTO
            };

            return response;
        }
    }
}