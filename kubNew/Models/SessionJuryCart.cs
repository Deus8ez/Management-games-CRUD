using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using kubNew.Infrastructure;

namespace kubNew.Models
{
    public class SessionJuryCart : JuryCart
    {
        public static JuryCart GetJuryCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
            .HttpContext.Session;
            SessionJuryCart cart = session?.GetJson<SessionJuryCart>("JCart")
            ?? new SessionJuryCart();
            cart.Session = session;
            return cart;
        }

        [JsonIgnore]
        public ISession Session { get; set; }
        public override void AddJury(Jury jury)
        {
            base.AddJury(jury);
            Session.SetJson("JCart", this);
        }
        public override void RemoveLine(Jury jury)
        {
            base.RemoveLine(jury);
            Session.SetJson("JCart", this);
        }
        public override void Clear()
        {
            base.Clear();
            Session.Remove("JCart");
        }
    }
}
