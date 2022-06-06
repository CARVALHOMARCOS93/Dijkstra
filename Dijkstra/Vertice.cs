using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijkstra
{
    internal class Vertice
    {
        #region propriedades
        private string id;
        private Dictionary<Vertice, int> adj = new Dictionary<Vertice, int>();
        private int distancia;
        private bool visitou;
        private Vertice? anterior;
        #endregion

        #region metodos
        public Vertice(string id)
        {
            this.id = id;
        }
        public string Id
        {
            get
            {
                return id;
            }
        }

        public int Distancia { get => distancia; set => distancia = value; }
        public bool Visitou { get => visitou; set => visitou = value; }
        internal Vertice? Anterior { get => anterior; set => anterior = value; }

        public void InserirAdjacencia(Vertice vertice, int custo) 
        {
            adj[vertice] = custo;
        }
        public Dictionary<Vertice, int> ObterAdjacentes()
        {
            return adj.OrderBy(x => x.Value).Where(x => x.Value != 0).ToDictionary(x => x.Key, x => x.Value);
        }
        public int ObterCusto(Vertice v) 
        {
            return adj[v];
        }

        #endregion

    }
}
