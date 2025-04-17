using AJFIlfov.Common;
using AJFIlfov.DataAccess.EntityFramework;
using AJFIlfov.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJFIlfov.DataAccess
{
    public class UnitOfWork
    {
        private readonly AjfilfovContext Context;

        public UnitOfWork(AjfilfovContext context)
        {
            this.Context = context;
        }

        private IRepository<Utilizatori>? users;
        public IRepository<Utilizatori> Users => users ?? (users = new BaseRepository<Utilizatori>(Context));

        private IRepository<PasswordRecovery> passwordRecoveries;
        public IRepository<PasswordRecovery> PasswordRecoveries => passwordRecoveries ?? (passwordRecoveries = new BaseRepository<PasswordRecovery>(Context));
        private IRepository<Disponibilitate> disponibilitate;
        public IRepository<Disponibilitate> Disponibilitate => disponibilitate ?? (disponibilitate = new BaseRepository<Disponibilitate>(Context));
        private IRepository<UserAddress> userAddresses;
        public IRepository<UserAddress> UserAddresses => userAddresses ?? (userAddresses = new BaseRepository<UserAddress>(Context));

        private IRepository<DisponibilitateAdmin> disponibilitateAdmin;
        public IRepository<DisponibilitateAdmin> DisponibilitateAdmin => disponibilitateAdmin ?? (disponibilitateAdmin = new BaseRepository<DisponibilitateAdmin>(Context));

        private IRepository<Meciuri> meciuri;
        public IRepository<Meciuri> Meciuri => meciuri ?? (meciuri = new BaseRepository<Meciuri>(Context));
        private IRepository<Anunt> anunturi;
        public IRepository<Anunt> Anunturi => anunturi ?? (anunturi = new BaseRepository<Anunt>(Context));

        private IRepository<Echipe> echipe;
        public IRepository<Echipe> Echipe => echipe ?? (echipe = new BaseRepository<Echipe>(Context));
        private IRepository<MeciLive> meciuriLive;
        public IRepository<MeciLive> MeciuriLive => meciuriLive ?? (meciuriLive = new BaseRepository<MeciLive>(Context));
        private IRepository<Grupe> grupe;
        public IRepository<Grupe> Grupe => grupe ?? (grupe = new BaseRepository<Grupe>(Context));
        private IRepository<GrupeEchipa> grupeEchipa;
        public IRepository<GrupeEchipa> GrupeEchipa => grupeEchipa ?? (grupeEchipa = new BaseRepository<GrupeEchipa>(Context));
        private IRepository<Stadioane> stadioane;
        public IRepository<Stadioane> Stadioane => stadioane ?? (stadioane = new BaseRepository<Stadioane>(Context));

        private IRepository<Localitati> localitati;
        public IRepository<Localitati> Localitati => localitati ?? (localitati = new BaseRepository<Localitati>(Context));
        private IRepository<StadionLocalitate> stadionLocalitate;
        public IRepository<StadionLocalitate> StadionLocalitate => stadionLocalitate ?? (stadionLocalitate = new BaseRepository<StadionLocalitate>(Context));

        private IRepository<Audit> audits;
        public IRepository<Audit> Audits => audits ?? (audits = new BaseRepository<Audit>(Context));

        private IRepository<Document> documents;
        public IRepository<Document> Documente => documents ?? (documents = new BaseRepository<Document>(Context));

        private IRepository<Answer> answers;
        public IRepository<Answer> Answers => answers ?? (answers = new BaseRepository<Answer>(Context));

        private IRepository<Question> questions;
        public IRepository<Question> Questions => questions ?? (questions = new BaseRepository<Question>(Context));

        private IRepository<Suggestion> suggestions;
        public IRepository<Suggestion> Suggestions => suggestions ?? (suggestions = new BaseRepository<Suggestion>(Context));


        private IRepository<Appointment> appointments;
        public IRepository<Appointment> Appointments => appointments ?? (appointments = new BaseRepository<Appointment>(Context));


        private IRepository<Invoice> invoices;
        public IRepository<Invoice> Invoices => invoices ?? (invoices = new BaseRepository<Invoice>(Context));


        private IRepository<InvoiceItem> invoiceItems;
        public IRepository<InvoiceItem> InvoiceItems => invoiceItems ?? (invoiceItems = new BaseRepository<InvoiceItem>(Context));

        private IRepository<BlogPost> blogPost;
        public IRepository<BlogPost> BlogPosts => blogPost ?? (blogPost = new BaseRepository<BlogPost>(Context));

        private IRepository<Turnee> turnee;
        public IRepository<Turnee> Turnee => turnee ?? (turnee = new BaseRepository<Turnee>(Context));
        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
