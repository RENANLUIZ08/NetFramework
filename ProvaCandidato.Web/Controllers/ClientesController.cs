using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProvaCandidato.Data;
using ProvaCandidato.Data.Entidade;
using ProvaCandidato.Helper;

namespace ProvaCandidato.Controllers
{
    public class ClientesController : Controller
    {
        private ContextoPrincipal db = new ContextoPrincipal();

        // GET: Clientes
        public ActionResult Index()
        {
            var clientes = db.Clientes.Include(c => c.Cidade);
            return View(clientes.ToList());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            ViewBag.obs = new List<ClienteObservacao>();
            ViewBag.CidadeId = new SelectList(db.Cidades, "Codigo", "Nome");
            return View();
        }

        // POST: Clientes/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Nome,DataNascimento,CidadeId,Ativo")] Cliente cliente)
        {
            var listObs = Request.Form["lsObs"];
            if (!string.IsNullOrEmpty(listObs))
            {
                string[] lObs = listObs.Split(',');
                List<ClienteObservacao> clienteObservacaos = new List<ClienteObservacao>();
                lObs.ToList().ForEach(t =>
                {
                    clienteObservacaos.Add(new ClienteObservacao()
                    {
                        Observacao = t
                    });
                });
                cliente.Observacoes = clienteObservacaos;
            }

            if (ModelState.IsValid)
            {
                db.Clientes.Add(cliente);
                db.SaveChanges();
                MessageHelper.DisplaySuccessMessage(this, "Cliente cadastrado com sucesso.");
                return RedirectToAction("Index");
            }

            ViewBag.CidadeId = new SelectList(db.Cidades, "Codigo", "Nome", cliente.CidadeId);
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Include(c => c.Observacoes).Where(c => c.Codigo == id)?.FirstOrDefault();
            if (cliente == null)
            {
                return HttpNotFound();
            }
            ViewBag.CidadeId = new SelectList(db.Cidades, "Codigo", "Nome", cliente.CidadeId);
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Nome,DataNascimento,CidadeId,Ativo")] Cliente cliente)
        {
            var listObs = Request.Form["lsObs"];
            if (!string.IsNullOrEmpty(listObs))
            {
                var cli = db.Clientes.Include(c => c.Observacoes).Where(c => c.Codigo == cliente.Codigo)?.FirstOrDefault();
                if (cli == null)
                { return HttpNotFound(); }
                else
                {
                    string[] lObs = listObs.Split(',');
                    var obsAtuais = cli.Observacoes;
                    lObs.ToList().ForEach(c =>
                    {
                        if(!obsAtuais.Any(d => d.Observacao == c))
                        {
                            cli.Observacoes.Add(new ClienteObservacao()
                            {
                                Observacao = c,
                                IdCliente = cli.Codigo
                            });
                        }

                    });

                    if (ModelState.IsValid)
                    {
                        db.Entry(cli).State = EntityState.Modified;
                        db.SaveChanges();
                        MessageHelper.DisplaySuccessMessage(this, $"Cliente {cli.Nome} foi alterado com sucesso.");
                        return RedirectToAction("Index");
                    }
                }
            }



            ViewBag.CidadeId = new SelectList(db.Cidades, "Codigo", "Nome", cliente.CidadeId);
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Clientes.Find(id);
            db.Clientes.Remove(cliente);
            db.SaveChanges();
            MessageHelper.DisplaySuccessMessage(this, $"Cidade foi excluida com sucesso.");
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
