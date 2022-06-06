using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijkstra
{
    internal class Grafo
    {
        #region propriedades
        private Dictionary<string, Vertice> vertices = new();
        private bool ehDirecionado;
        #endregion
        #region metodos
        public Grafo(bool ehDirecionado)
        {
            this.ehDirecionado = ehDirecionado;
        }
        public void AdicionarVertices(string id) 
        {
            vertices[id] = new Vertice(id);
        }
        public void AdicionarAresta(string origem, string destino, int custo) 
        {
            vertices[origem].InserirAdjacencia(vertices[destino], custo);
            if (!ehDirecionado) 
            {
                vertices[destino].InserirAdjacencia(vertices[origem], custo);
            }
        }
        public List<Tuple<Vertice, Vertice>> ObterArestas() 
        {
            List<Tuple<Vertice, Vertice>> result = new List<Tuple<Vertice, Vertice>>();
            foreach (KeyValuePair<string, Vertice> kvp in vertices) 
            {
                foreach (KeyValuePair<Vertice, int> vertice in kvp.Value.ObterAdjacentes()) 
                {
                    result.Add(Tuple.Create(kvp.Value, vertice.Key));
                }
            }

            return result;
        }
        public Vertice? ObterVertice(string chave) 
        {
            return vertices.ContainsKey(chave) ? vertices[chave] : null;
        }
        public Dictionary<string, Vertice> ObterVertices() 
        {
            return vertices;
        }

        public void DefinirVertices(Dictionary<string, Vertice> vertices)
        {
            this.vertices = vertices;
        }
        #endregion
    }
}
