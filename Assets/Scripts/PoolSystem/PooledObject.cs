using Game.Manager;
using Game.Utilities.Constants;

namespace Game.Object
{
    public abstract class PooledObject : CustomBehaviour
    {
        private DeactiveParents m_DeactiveParent;
        public PooledObjectType PooledObjectTag { get; private set; }

        private LevelManager m_LevelManager;

        public override void Initialize()
        {
            m_LevelManager = GameManager.Instance.GetManager<LevelManager>();
        }

        public virtual void OnObjectSpawn()
        {
            m_LevelManager.OnCleanSceneObject += OnObjectDeactive;
        }

        public virtual void OnObjectDeactive()
        {
            m_LevelManager.OnCleanSceneObject -= OnObjectDeactive;
            //GameManager.Instance.ObjectPool.AddObjectPool(PooledObjectTag, this);
            //this.transform.SetParent(GameManager.Instance.Entities.GetDeactiveParent(m_DeactiveParent));
            //this.gameObject.SetActive(false);
        }

        public void SetDeactiveParent(DeactiveParents _deactiveParent)
        {
            m_DeactiveParent = _deactiveParent;
        }

        public void SetPooledObjectType(PooledObjectType _type)
        {
            PooledObjectTag = _type;
        }

        protected virtual void OnDestroy()
        {
            m_LevelManager.OnCleanSceneObject -= OnObjectDeactive;
        }
    }
}
