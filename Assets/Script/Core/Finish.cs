using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Script.Core
{
    public class Finish : MonoBehaviour
    {
        private HashSet<GameObject> _players = new HashSet<GameObject>();
        private IMainManager _mainManager;


        [Inject]
        private void Construct(IMainManager mainManager)
        {
            _mainManager = mainManager;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _players.Add(other.gameObject);
                _mainManager.UpdatedFinishingAmount(_players.Count);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _players.Remove(other.gameObject);
            }
        }
    }
}