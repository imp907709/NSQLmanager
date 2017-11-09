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
    public interface ITokenBuilderNoGen
    {
        List<ITypeToken> Command(ITypeToken command_, IOrientVertex orientClass_, ITypeToken orientObjectToken_, ITypeToken content = null);
        List<ITypeToken> Command(ITypeToken command_, IOrientVertex orientClass_, ITypeToken orientObject, ITypeToken from, ITypeToken to, ITypeToken content = null);
        List<ITypeToken> Command(ITypeToken command_, IOrientVertex orientClass_, ITypeToken orientObject);

    }
    public interface ITokenBuilderTypeGen
    {   

        List<ITypeToken> Command(ITypeToken command_, IOrientVertex orientClass_, ITypeToken content = null);
        List<ITypeToken> Command(ITypeToken command_, IOrientVertex orientClass_,  ITypeToken from, ITypeToken to, ITypeToken content = null);
        List<ITypeToken> Command(ITypeToken command_, IOrientVertex orientClass_);

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