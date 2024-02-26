namespace N5.Business.Interfaces.Services
{
    using System;

    public interface IElasticsearchService<T> where T : class
    {

        /// <summary>
        /// Retrieves all documents from Elasticsearch.
        /// </summary>
        /// <returns>A list of documents retrieved from Elasticsearch.</returns>
        Task<IList<T>> GetAllAsync();

        /// <summary>
        /// Retrieves a document by its ID from Elasticsearch.
        /// </summary>
        /// <param name="id">The ID of the document to retrieve.</param>
        /// <returns>The document retrieved from Elasticsearch.</returns>
        Task<T> FirstAsync(int id);

        /// <summary>
        /// Inserts a document into Elasticsearch.
        /// </summary>
        /// <param name="data">The document to insert.</param>
        Task InsertAsync(T data);

        /// <summary>
        /// Updates a document in Elasticsearch.
        /// </summary>
        /// <param name="data">The updated document.</param>
        /// <param name="id">The ID of the document to update.</param>
        Task UpdateAsync(T data, int id);

        /// <summary>
        /// Delete a document by ID from Elasticsearch.
        /// </summary>
        /// <param name="id">The ID of the document to delete.</param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Deletes the entire index from Elasticsearch.
        /// </summary>
        Task DeleteIndexAsync();
    }
}