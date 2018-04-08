namespace DocsByReflection.UnitTests.Stubs
{
    public abstract class StubBase<TKey, TEntity>
    {
        #region Fields
        /// <summary>
        /// Gets or sets FieldGenericOnBaseClassWithDoc.
        /// </summary>
        protected TKey FieldGenericOnBaseClassWithDoc;

        /// <summary>
        /// Gets or sets FieldOnBaseClassWithDoc.
        /// </summary>
        protected string FieldOnBaseClassWithDoc;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets PropertyGenericOnBaseClassWithDoc.
        /// </summary>
        public TKey PropertyGenericOnBaseClassWithDoc { get; set; }

        /// <summary>
        /// Gets or sets PropertyOnBaseClassWithDoc.
        /// </summary>
        public string PropertyOnBaseClassWithDoc { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The entity with new created id.</returns>
        public TEntity Create(TEntity entity)
        {
            FieldOnBaseClassWithDoc = null;
            FieldGenericOnBaseClassWithDoc = default(TKey);
            return entity;
        }
        #endregion
    }
}
