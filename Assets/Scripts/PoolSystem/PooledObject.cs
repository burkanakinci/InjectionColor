using Game.Manager;
using Game.Utilities.Constants;

namespace Game.Object
{
    public abstract class PooledObject : CustomBehaviour
    {
        private DeactiveParents m_DeactiveParent;
        public string PooledObjectTag { get; private set; }

        public override void Initialize()
        {
        }

        public virtual void OnObjectSpawn()
        {
            //GameManager.Instance.GetManager<LevelManager>().OnCleanSceneObject += OnObjectDeactive;
        }

        public virtual void OnObjectDeactive()
        {
            //GameManager.Instance.LevelManager.OnCleanSceneObject -= OnObjectDeactive;
            //GameManager.Instance.ObjectPool.AddObjectPool(PooledObjectTag, this);
            //this.transform.SetParent(GameManager.Instance.Entities.GetDeactiveParent(m_DeactiveParent));
            //this.gameObject.SetActive(false);
        }

        public CustomBehaviour GetGameObject()
        {
            return this;
        }

        public void SetDeactiveParent(DeactiveParents _deactiveParent)
        {
            m_DeactiveParent = _deactiveParent;
        }

        public void SetPooledObjectTag(string _tag)
        {
            PooledObjectTag = _tag;
        }

        protected virtual void OnDestroy()
        {
            //GameManager.Instance.LevelManager.OnCleanSceneObject -= OnObjectDeactive;
        }
    }
}
