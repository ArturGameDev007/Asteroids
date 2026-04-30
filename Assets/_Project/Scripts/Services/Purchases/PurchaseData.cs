using System;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Services.Purchases
{
    public class PurchaseData
    {
        public string Id { get; private set; }
        public Func<UniTaskVoid> TypeAction { get; private set; }

        public PurchaseData(string id, Func<UniTaskVoid> typeAction)
        {
            Id = id;
            TypeAction = typeAction;
        }
    }
}