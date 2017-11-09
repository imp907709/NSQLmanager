using System.Collections.Generic;

using IOrientObjects;

namespace IQueryManagers
{
   
    /// <summary>
    /// Main manager interfaces
    /// </summary>

    /// <summary>
    /// Tokens for Orient API URIs 
    /// Different API types tend to different Http req strategies example: Fucntion/param or: Batch/ + JSON-body
    /// (add types to ItypeToken for plugging-in)
    /// </summary>
    //For token items
    public interface ITypeToken
    {
        string Text { get; set; }
    }
    public interface ITokenBuilder
    {
        List<ITypeToken> Command(ITypeToken command_, ITypeToken orientObject, ITypeToken orientContext = null);
        List<ITypeToken> Command(ITypeToken command_, IOrientObject orientObject, ITypeToken from, ITypeToken to, ITypeToken orientContext = null);
        List<ITypeToken> Command(ITypeToken command_, IOrientObject orientObject, IOrientObject orientClass_, ITypeToken orientTypeClass_, bool mandatory = false, bool notNull = false);

    }
    public interface ITokenBuilderImplicit
    {

        List<ITypeToken> Command(ITypeToken command_, ITypeToken orientObject);
        List<ITypeToken> Command(ITypeToken command_, ITypeToken orientObject, ITypeToken orientType);
        List<ITypeToken> Command(ITypeToken command_, ITypeToken orientObject, ITypeToken orientType, ITypeToken context_);
        List<ITypeToken> Command(ITypeToken command_, ITypeToken orientObject, ITypeToken orientType, ITypeToken tokenA, ITypeToken tokenB, ITypeToken content);

    }
    //Building Item from Token types
    public interface ITextBuilder
    {
        ITypeToken Text { get; }
        ITypeToken FormatPattern { get; }
        List<ITypeToken> Tokens { get; }

        string Build(List<ITypeToken> tokens_, ITypeToken FormatPattern_);
        string GetText();
        void SetText(List<ITypeToken> tokens_, ITypeToken FormatPattern_);      
        
    }
    
}