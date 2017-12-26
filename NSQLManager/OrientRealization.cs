﻿
using System;
using System.Collections.Generic;

using System.Linq;

using System.Net;

using System.Configuration;

using WebManagers;
using IQueryManagers;
using QueryManagers;
using IOrientObjects;

using POCO;
using System.Reflection;
using Newtonsoft.Json.Converters;

/// <summary>
/// Realization of IJsonMangers, IWebManagers, and IOrient specifically for orient db
/// </summary>
namespace OrientRealization
{

    /// <summary>
    ///     Orient specific WebManager for authentication and authenticated responses sending to URL
    ///     with NetworkCredentials
    /// </summary>    

    public class OrientWebManager : WebManager
    {
        string OSESSIONID=string.Empty;

        //>> add async
        public new HttpWebResponse GetResponse(string url, string method)
        {

            //HttpWebResponse resp=null;
            base.AddRequest(url, method);
            base.AddHeader(HttpRequestHeader.Cookie, this.OSESSIONID);

            try
            {
                return (HttpWebResponse)this._request.GetResponse();
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine(e);
            }

            return null;
        }
        public HttpWebResponse GetResponseCred(string method_)
        {

            //HttpWebResponse resp=null; base.addRequest(url, method);
            this._request.Method=method_;
            try
            {
                return (HttpWebResponse)this._request.GetResponse();
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine(e);
            }

            return null;
        }
        public WebResponse Authenticate(string url, NetworkCredential nc=null)
        {

            WebResponse resp;
            AddRequest(url, "GET");
            if (nc==null) {CredentialsBind();}
            else {SetCredentials(nc);}
            try
            {
                resp=this._request.GetResponse();
                OSESSIONID=GetHeaderValue("Set-Cookie");
                return resp;
            }
            catch (Exception e)
            {
                throw e;
           }

        }

    }

    #region Tokens

    /// <summary>
    ///  Tokens realization for different string concatenations
    /// </summary>

    //Tokens for Orient Comamnd and Authenticate URLs
    public class OrientHost : ITypeToken
    {
        public string Text {get; set;}=ConfigurationManager.AppSettings["OrientDevHost"];
    }
    public class OrientDatabaseNameToken : ITypeToken
    {
        public string Text {get; set;}=ConfigurationManager.AppSettings["OrientUnitTestDB"];
    }
    public class OrientPort : ITypeToken
    {
        public string Text {get; set;}="2480";
    }
    public class OrientDatabaseCRUDToken : ITypeToken
    {
        public string Text {get; set;}="Connect";
    }
    public class OrientAuthenticateToken : ITypeToken
    {
        public string Text {get; set;}="connect";
    }
    public class OrientCommandToken : ITypeToken
    {
        public string Text {get; set;}="command";
    }
    public class OrientCommandSQLTypeToken : ITypeToken
    {
        public string Text {get; set;}="sql";
    }
    public class OrientFuncionToken : ITypeToken
    {
        public string Text {get; set;}="function";
    }
    public class OrientBatchToken : ITypeToken
    {
        public string Text {get; set;}="batch";
    }
    public class OrientTraverseToken : ITypeToken
    {
        public string Text {get; set;}="traverse";
    }

    /// <summary>
    /// Orient query tokens
    /// </summary>  

    //Orient SQL syntax tokens
    public class OrientURLDatabaseToken : ITypeToken
    {
        public string Text {get; set;}="database";
    }
    public class OrientPlocalToken : ITypeToken
    {
        public string Text {get; set;}="plocal";
    }
    public class OrientExpandToken : ITypeToken
    {
        public string Text {get; set;}="expand";
    }
    public class OrientAsToken : ITypeToken
    {
        public string Text { get; set; } = "as";
    }
    public class OrientContentToken : ITypeToken
    {
        public string Text {get; set;}="content";
    }
    public class OrientEdgeToken : ITypeToken
    {
        public string Text {get; set;}="Edge";
    }
    public class OrientPropertyToken : ITypeToken
    {
        public string Text {get; set;}="Property";
    }
    public class OrientToToken : ITypeToken
    {
        public string Text {get; set;}="To";
    }
    public class OrientInToken : ITypeToken
    {
        public string Text {get; set;}="in";
    }
    public class OrientOutToken : ITypeToken
    {
        public string Text {get; set;}="out";
    }
    public class OrientEToken : ITypeToken
    {
        public string Text {get; set;}="E";
    }
    public class OrientVToken : ITypeToken
    {
        public string Text {get; set;}="V";
    }
    public class OrientVertexToken : ITypeToken
    {
        public string Text {get; set;}="Vertex";
    }
    public class OrientExtendsToken : ITypeToken
    {
        public string Text {get; set;}="Extends";
    }
    public class OrientMandatoryToken : ITypeToken
    {
        public string Text {get; set;}="MANDATORY";
    }
    public class OrientNotNULLToken : ITypeToken
    {
        public string Text {get; set;}="NOTNULL";
    }
    //Orient URL command and batch body tokens 
    public class OrientBodyConnectToken : ITypeToken
    {
        public string Text {get; set;}="connect";
    }
    public class OrientBodyCommandToken : ITypeToken
    {
        public string Text {get; set;}="command";
    }
    public class OrientBodyTransactionToken : ITypeToken
    {
        public string Text {get; set;}="transaction";
    }
    public class OrientBodyOperationToken : ITypeToken
    {
        public string Text {get; set;}="operations";
    }
    public class OrientBodyTypeToken : ITypeToken
    {
        public string Text {get; set;}="type";
    }
    public class OrientBodyLanguageToken : ITypeToken
    {
        public string Text {get; set;}="language";
    }
    public class OrientBodyScriptToken : ITypeToken
    {
        public string Text {get; set;}="script";
    }
    public class OrientDogToken : ITypeToken
    {
        public string Text {get; set;}="@";
    }

    
    //SQL tokens
    public class OrientSelectToken : ITypeToken
    {
        public string Text {get; set;}="Select";
    }
    public class OrientFromToken : ITypeToken
    {
        public string Text {get; set;}="from";
    }
    public class OrientWhereToken : ITypeToken
    {
        public string Text {get; set;}="where";
    }
    public class OrientUUIDToken : ITypeToken
    {
        public string Text {get; set;}= "uuid()";
    }
    public class OrientAlterToken : ITypeToken
    {
        public string Text { get; set; } = "alter";
    }
    public class OrientDeleteToken : ITypeToken
    {
        public string Text {get; set;}="Delete";
    }
    public class OrientDropToken : ITypeToken
    {
        public string Text {get; set;}="Drop";
    }
    public class OrientDefaultToken : ITypeToken
    {
        public string Text { get; set; } = "default";
    }
    public class OrientCreateToken : ITypeToken
    {
        public string Text { get; set; } = "create";
    }
    public class OrientUpdateToken : ITypeToken
    {
      public string Text { get; set; } = "update";
    }
    public class OrientBetweenToken : ITypeToken
    {
      public string Text {get;set;}="between";
    }


    //Overall command tokens
    public class OrientTypeToken : ITypeToken
    {
        public string Text {get; set;}="Type";
    }
    public class OrientAndToken : ITypeToken
    {
        public string Text {get; set;}=@"and";
    }
    public class OrientClassToken : ITypeToken
    {
        public string Text {get; set;}="Class";
    }
    public class OrientEqualsToken : ITypeToken
    {
        public string Text {get; set;}=@"=";
    }
    public class OrientApostropheToken : ITypeToken
    {
        public string Text {get; set;}=@"'";
    }
    public class OrientDoubleQuotesToken : ITypeToken
    {
        public string Text {get; set;}=@"""";
    }
    public class OrientGapToken : ITypeToken
    {
        public string Text {get; set;}=@" ";
    }
    public class OrientDotToken : ITypeToken
    {
        public string Text {get; set;}=@".";
    }
    public class OrientCommaToken : ITypeToken
    {
        public string Text {get; set;}=@",";
    }
    public class OrientRoundBraketLeftToken : ITypeToken
    {
        public string Text {get; set;}=@"(";
    }
    public class OrientRoundBraketRightToken : ITypeToken
    {
        public string Text {get; set;}=@")";
    }
    public class OrientFigureBraketLeftToken : ITypeToken
    {
        public string Text {get; set;}=@"{";
    }
    public class OrientFigureBraketRightToken : ITypeToken
    {
        public string Text {get; set;}=@"}";
   }
    public class OrientSquareBraketLeftToken : ITypeToken
    {
        public string Text {get; set;}=@"[";
   }
    public class OrientSquareBraketRightToken : ITypeToken
    {
        public string Text {get; set;}=@"]";
   }
    public class OrientTRUEToken : ITypeToken
    {
        public string Text {get; set;}=@"true";
   }
    public class OrientFLASEToken : ITypeToken
    {
        public string Text {get; set;}=@"FALSE";
   }
    

    //URI tokens
    public class ColonToken : ITypeToken
    {
        public string Text {get; set;}=":";
   }
    public class SlashToken : ITypeToken
    {
        public string Text {get; set;}="\\";
   }
    public class BackSlashToken : ITypeToken
    {
        public string Text {get; set;}="/";
   }
   

    public class OrientIdSharpToken : ITypeToken
    {
        public string Text {get; set;}=@"#";
   }
    public class OrientIdToken : ITypeToken
    {
        public string Text {get; set;}="Id";
   }

    //Orient functions tokens
    public class OrientUUIDoken : ITypeToken
    {
        public string Text { get; set; } = "uuid();";
    }

    //orient dateformats tokens
    public class OrientSTRINGToken : ITypeToken
    {
        public string Text {get; set;}="STRING";
   }
    public class OrientBOOLEANToken : ITypeToken
    {
        public string Text { get; set; } = "BOOLEAN";
    }
    public class OrientDATEToken : ITypeToken
    {
        public string Text {get; set;}="DATE";
   }
    public class OrientINTEGERToken : ITypeToken
    {
        public string Text {get; set;}="INTEGER";
   }
    public class OrientDATETIMEToken : ITypeToken
    {
        public string Text {get; set;}="DATETIME";
   }
    public class OrientIntToken : ITypeToken
    {
        public string Text { get; set; } = "INTEGER";
    }

    
    //orient Class tokens
    public class OrientPersonToken : ITypeToken
    {
        public string Text {get; set;}="Person";
   }
    public class OrientUnitToken : ITypeToken
    {
        public string Text {get; set;}="Unit";
   }
    public class OrientSubUnitToken : ITypeToken
    {
        public string Text {get; set;}="SubUnit";
   }
    public class OrientMainAssignmentToken : ITypeToken
    {
        public string Text {get; set;}="MainAssignment";
   }
    public class OrientTrackBirthdaysToken : ITypeToken
    {
        public string Text {get; set;}="TrackBirthdays";
   }
    public class OrientNameToken : ITypeToken
    {
        public string Text {get; set;}="Name";
   }
    public class OrientAccountToken : ITypeToken
    {
        public string Text {get; set;}="sAMAccountName";
   }
    public class OrientBrewery : ITypeToken
    {
        public string Text {get; set;}="Brewery";
   }
    public class OrientUserSettingsToken : ITypeToken
    {
        public string Text {get; set;}="UserSettings";
   }
    public class OrientCommonSettingsToken : ITypeToken
    {
        public string Text {get; set;}="CommonSettings";
   }
    public class OrientTestDbToken : ITypeToken
    {
        public string Text {get; set;}=ConfigurationManager.AppSettings["OrientUnitTestDB"];
   }
    public class OrientNoteToken : ITypeToken
    {
        public string Text { get; set; } = "Note";
    }
    public class OrientAuthorshipToken : ITypeToken
    {
        public string Text { get; set; } = "Authorship";
    }    
    public class OrientCommentToken : ITypeToken
    {
        public string Text { get; set; } = "Comment";
    }
    public class OrientLikeToken : ITypeToken
    {
        public string Text { get; set; } = "Like";
    }
    public class OrientTaggedToken : ITypeToken
    {
        public string Text { get; set; } = "Tagged";
    }
    public class OrientTagToken : ITypeToken
    {
        public string Text { get; set; } = "Tag";
    }
    public class OrientObjectScToken : ITypeToken
    {
        public string Text { get; set; } = "Object_SC";
    }
    
    public class UOWDeletedToken : ITypeToken
    {
        public string Text { get; set; } = "Deleted";
    }
    public class UOWCreatedToken : ITypeToken
    {
        public string Text { get; set; } = "Created";
    }
    ///<summary>
    ///Web response reader Tokens
    /// </summary>

    public class RESULT : ITypeToken
    {
        public string Text {get; set;}="result";
   }
    #endregion

    #region Formats

    public class PropertyItemFormat : ITypeToken
    {
        public string Text {get;set;}=@"{0} {1}{2}{3}";
    }
    
    #endregion

    #region Factories

    public class OrientQueryFactory : TokenMiniFactory, IOrientQueryFactory
    {

        public ITypeToken AlterToken()
        {
            return new OrientAlterToken();
        }
        public ITypeToken CreateToken()
        {
            return new OrientCreateToken();
        }
        public ITypeToken DefaultToken()
        {
            return new OrientDefaultToken();
        }
        public ITypeToken UUIDToken()
        {
            return new OrientUUIDToken();
        }

        public ITypeToken DoubleQuotes()
        {
            return new OrientDoubleQuotesToken();
        }

        public ITypeToken DeleteToken()
        {
            return new OrientDeleteToken();
        }

        public ITypeToken UpdateToken()
        {
            return new OrientUpdateToken();
        }

        public ITypeToken PropertyItemFormatToken()
        {
            return new PropertyItemFormat();
       }

        public ITypeToken SelectToken()
        {
            return new OrientSelectToken ();
       }
        public ITypeToken TraverseToken()
        {
          return new OrientTraverseToken();
        }

        public ITypeToken FromToken()
        {
            return new OrientFromToken();
        }
        public ITypeToken ToToken()
        {
            return new OrientToToken();
        }

        public ITypeToken WhereToken()
        {
            return new OrientWhereToken();
        }
        public ITypeToken AndToken()
        {
          return new OrientAndToken();
        }

        public ITypeToken Between()
        {
            return new OrientBetweenToken();
        }

        public ITypeToken ClassToken()
        {
          return new OrientClassToken();
        }
        public ITypeToken ExtendsToken()
        {
          return new OrientExtendsToken();
        }
        public ITypeToken ContentToken()
        {
          return new OrientContentToken();
        }

        public ITypeToken VertexToken()
        {
          return new OrientVertexToken();
        }
        public ITypeToken EdgeToken()
        {
          return new OrientEdgeToken();
        }

        public ITypeToken PropertyToken()
        {
          return new OrientPropertyToken();
        }
        public ITypeToken PropertyTypeToken()
        {
          return new OrientTypeToken();
        }

        public ITypeToken LeftRoundBraket()
        {
          return new OrientRoundBraketLeftToken();
        }
        public ITypeToken RightRoundBraket()
        {
          return new OrientRoundBraketRightToken();
        }
        public ITypeToken LeftSquareBraket()
        {
          return new OrientSquareBraketLeftToken();
        }
        public ITypeToken RightSquareBraket()
        {
          return new OrientSquareBraketRightToken();
        }

        public ITypeToken Equals()
        {
          return new OrientEqualsToken();
        }

        public ITypeToken In()
        {
          return new OrientInToken();
        }
        public ITypeToken Out()
        {
          return new OrientOutToken();
        }
        public ITypeToken E()
        {
          return new OrientEToken();
        }
        public ITypeToken V()
        {
          return new OrientVToken();
        }

        public ITypeToken As()
        {
            return new OrientAsToken();
        }
        public ITypeToken Dog()
        {
            return new OrientDogToken();
        }

        public ITypeToken Expand()
        {
            return new OrientExpandToken();
        }

        public ITypeToken Mandatory()
        {
            return new OrientMandatoryToken();
       }
        public ITypeToken NotNull()
        {
            return new OrientNotNULLToken();
       }

    }
    public class OrientBodyFactory : IOrientBodyFactory
    {

        public ITypeToken PLocal()
        {
            return new OrientPlocalToken();
       }
        public ITypeToken Connect()
        {
            return new OrientBodyConnectToken();
       }
        public ITypeToken Comma()
        {
            return new OrientCommaToken();
       }
        public ITypeToken True()
        {
            return new OrientTRUEToken();
       }
        public ITypeToken False()
        {
            return new OrientFLASEToken();
        }
        public ITypeToken Content()
        {
            return new OrientContentToken();
       }

        // /
        public ITypeToken BackSlash()
        {
            return new BackSlashToken();
        }

        // \
        public ITypeToken Slash()
        {
            return new SlashToken();
        }

        public ITypeToken LeftSqGap()
        {
            return new OrientSquareBraketLeftToken();
       }
        public ITypeToken RightSqGap()
        {
            return new OrientSquareBraketRightToken();
       }

        //:
        public ITypeToken Colon()
        {
            return new ColonToken();
       }      
        // {
        public ITypeToken LeftFgGap()
        {
            return new OrientFigureBraketLeftToken();
       }
        //}
        public ITypeToken RightFgGap()
        {
            return new OrientFigureBraketRightToken();
       }
        // "
        public ITypeToken Quotes()
        {
            return new OrientDoubleQuotesToken();
       }

        public ITypeToken Batch()
        {
            return new OrientBatchToken();
       }

        public ITypeToken Database()
        {
            return new OrientURLDatabaseToken();
       }

        public ITypeToken Command()
        {
            return new OrientBodyCommandToken();
       }
        public ITypeToken Transactions()
        {
            return new OrientBodyTransactionToken();
       }
        public ITypeToken Operations()
        {
            return new OrientBodyOperationToken();
       }
        public ITypeToken Type()
        {
            return new OrientBodyTypeToken();
       }

        public ITypeToken Language()
        {
            return new OrientBodyLanguageToken();
       }
        public ITypeToken sql()
        {
            return new OrientCommandSQLTypeToken();
       }
        public ITypeToken Sctipt()
        {
            return new OrientBodyScriptToken();
       }


        public ITypeToken StringToken()
        {
            return new OrientSTRINGToken();
        }
        public ITypeToken BooleanToken()
        {
            return new OrientBOOLEANToken();
        }
    }    

    #endregion

    
    #region Schemas

    /// <summary>
    /// Base class for shemas building.
    /// Not abstract, because can be used for manual command generation
    /// </summary>
    public class Shemas
    {
        internal ICommandFactory _commandFactory;
        internal IFormatFactory _formatFactory;
        internal ITokenMiniFactory _miniFactory;

        internal ICommandBuilder _commandBuilder;    
        internal IFormatFromListGenerator _formatGenerator;
       
        internal string lastGeneneratedCommand;

        public Shemas(        
            ICommandBuilder commandBuilder
            , IFormatFromListGenerator formatGenerator
            , ITokenMiniFactory miniFactory_)
        {
            _commandBuilder=commandBuilder;
            _formatGenerator=formatGenerator;
            _miniFactory=miniFactory_;
       }

        public Shemas(
            ICommandFactory commandFactory_
            , IFormatFactory formatFactory_
            , ITokenMiniFactory miniFactory_)
        {
            
            _commandFactory=commandFactory_;
            _formatFactory=formatFactory_;
            _miniFactory=miniFactory_;

            _commandBuilder=_commandFactory.CommandBuilder(_miniFactory, _formatFactory);
            _formatGenerator=formatFactory_.FormatGenerator(_miniFactory);
       }

        public void BuildOld(List<ITypeToken> tokenList_, ITypeToken format=null)
        {
            //tokenList_.Add(factory.NewEmptyString());
            this._commandBuilder.AddTokens(tokenList_);

            //generate format from token list with system.empty placeholder List<n> => "{0} ..{}.. {n}"
            ITypeToken token=_miniFactory.NewToken();
            if (format==null)
            {
                token=_formatGenerator.FromatFromTokenArray(tokenList_);
           }
            else {token=format;}
            this._commandBuilder.AddFormat(token);
            lastGeneneratedCommand=this._commandBuilder.Build().GetText();
       }
        public void ReBuildOld(List<ITypeToken> tokenList_, ITypeToken format=null)
        {
            //tokenList_.Add(factory.NewEmptyString());            
            this._commandBuilder.BindTokens(tokenList_);

            //generate format from token list with system.empty placeholder List<n> => "{0} ..{}.. {n}"
            ITypeToken token=_miniFactory.NewToken();
            if (format==null)
            {
                token=_formatGenerator.FromatFromTokenArray(tokenList_);
           }
            else {token=format;}

            this._commandBuilder.BindFormat(token);
            lastGeneneratedCommand=this._commandBuilder.Build().GetText();
       }
        
        public ICommandBuilder GetBuilder()
        {
            ICommandBuilder cb=_commandFactory.CommandBuilder(this._miniFactory,this._formatFactory);
            cb.BindTokens(this._commandBuilder.Tokens);
            cb.BindFormat(this._commandBuilder.FormatPattern);
            return cb.Build();
        }

        public ICommandBuilder BuildFormatNew(List<ICommandBuilder> tokenList_, ITypeToken format)
        {
            this._commandBuilder.BindBuilders(tokenList_, format);
            this._commandBuilder.Build();
            return GetBuilder();
       }
        public ICommandBuilder BuildFormatNew(List<ITypeToken> tokenList_, ITypeToken format)
        {
            this._commandBuilder.BindTokens(tokenList_);
            this._commandBuilder.BindFormat(format);
            this._commandBuilder.Build();
            return GetBuilder();
        }
        public ICommandBuilder BuildNew(List<ICommandBuilder> tokenList_, ITypeToken delimeter)
        {
            this._commandBuilder.BindBuilders(tokenList_, this._formatGenerator.FromatFromTokenArray(
                tokenList_, delimeter));
            return this._commandBuilder.Build();           
       }
        public ICommandBuilder BuildNew(List<ITypeToken> tokenList_, ITypeToken delimeter)
        {
            this._commandBuilder.BindTokens(tokenList_);
            this._commandBuilder.BindFormat(this._formatGenerator.FromatFromTokenArray(
                tokenList_, delimeter));
            this._commandBuilder.Build();
            return GetBuilder();
        }

        public ICommandBuilder AddLeft(ICommandBuilder b1, ICommandBuilder b2, ITypeToken delimeter)
        {
            List<ICommandBuilder> builder=new List<ICommandBuilder>() {b1, b2};
            ITypeToken format=this._formatGenerator.FromatFromTokenArray(builder, delimeter);          
            this._commandBuilder.BindBuilders(builder, format);
            this._commandBuilder.Build();
            return GetBuilder();
       }
        public ICommandBuilder AddRight(ICommandBuilder b1, ICommandBuilder b2, ITypeToken delimeter)
        {
            List<ICommandBuilder> builder=new List<ICommandBuilder>() {b2, b1};
            ITypeToken format=this._formatGenerator.FromatFromTokenArray(builder, delimeter);         
            this._commandBuilder.BindBuilders(builder, format);
            this._commandBuilder.Build();
            return GetBuilder();
       }

    }
    /// <summary>
    /// Shemas for different commands with parameters and builders.
    /// Needs CommandBuilder and FormatGenerator realizations.
    /// Command tokens generated in token factory.
    /// </summary>
    public class CommandShemasExplicit : Shemas
    {

        IOrientQueryFactory _queryFactory;

        //public CommandShemasExplicit(ICommandBuilder commandBuilder_
        //    , IFormatFromListGenerator formatListgenerator_
        //    , ITokenMiniFactory miniFactory_
        //    , IOrientQueryFactory queryfactory_)
        //    :base(commandBuilder_,formatListgenerator_,miniFactory_)
        //{
        //    this._commandBuilder=commandBuilder_;
        //    this._formatGenerator=formatListgenerator_;
        //    this.miniFactory=miniFactory_;
        //    this.orientFactory=queryfactory_;
        //}

        public CommandShemasExplicit(
            ICommandFactory commandFactory_
            , IFormatFactory formatFactory_
            , ITokenMiniFactory miniFactory_
            , IOrientQueryFactory queryfactory_)
            : base(commandFactory_, formatFactory_, miniFactory_)
        {

            this._commandFactory=commandFactory_;
            this._formatFactory=formatFactory_;
            this._miniFactory=miniFactory_;
            this._queryFactory=queryfactory_;

            this._commandBuilder=_commandFactory.CommandBuilder(base._miniFactory, _formatFactory);
            this._formatGenerator=formatFactory_.FormatGenerator(base._miniFactory);
         
       }

        public void Build(List<ITypeToken> tokenList_, ITypeToken format=null)
        {
            //tokenList_.Add(factory.NewEmptyString());            
            this._commandBuilder.AddTokens(tokenList_);
            
            //generate format from token list with system.empty placeholder List<n> => "{0} ..{}.. {n}"
            ITypeToken token=_miniFactory.NewToken();
            if (format==null)
            {
                token=_formatGenerator.FromatFromTokenArray(tokenList_);
           }
            else {token=format;}

            this._commandBuilder.AddFormat(token);
            lastGeneneratedCommand=this._commandBuilder.Build().GetText();
       }
        public void ReBuild(List<ITypeToken> tokenList_, ITypeToken format=null)
        {
            //tokenList_.Add(factory.NewEmptyString());            
            this._commandBuilder.BindTokens(tokenList_);

            //generate format from token list with system.empty placeholder List<n> => "{0} ..{}.. {n}"
            ITypeToken token=_miniFactory.NewToken();
            if (format==null)
            {
                token=_formatGenerator.FromatFromTokenArray(tokenList_);
           }
            else {token=format;}

            this._commandBuilder.BindFormat(token);
            lastGeneneratedCommand=this._commandBuilder.Build().GetText();
       }


        /// <summary>
        /// For default right handed command functionality (Extends,class,token e.t.c)
        /// </summary>
        /// <param name="token_">Command token</param>
        /// <param name="param_">Command parameters</param>
        /// <returns></returns>
        public void ParametrizedCommand(ITypeToken token_, ICommandBuilder param_)
        {
            List<ICommandBuilder> buildersList=new List<ICommandBuilder>();
            List<ITypeToken> tokens=new List<ITypeToken>();

            tokens.Add(token_);
            tokens.Add(_miniFactory.Gap());
            buildersList.Add(BuildNew(tokens, this._miniFactory.EmptyString()));
            if (param_ != null)
            {
                buildersList.Add(param_);               
           }
          
            this._commandBuilder=BuildNew(buildersList, this._miniFactory.EmptyString());          
       }
        public void ParametrizedCommand(List<ITypeToken> token_,ICommandBuilder param_,ITypeToken delimeter_=null)
        {
            List<ICommandBuilder> buildersList=new List<ICommandBuilder>();
            ITypeToken placeholder=null;
            if (delimeter_==null) {placeholder=this._miniFactory.EmptyString();}
            else {placeholder=delimeter_;}

            buildersList.Add(BuildNew(token_, placeholder));
            if (param_ != null)
            {
                buildersList.Add(param_);
           }

            this._commandBuilder=BuildNew(buildersList, this._miniFactory.EmptyString());
       }
        public void ParametrizedCommand(ITypeToken token_,ITypeToken param_)
        {
            List<ICommandBuilder> buildersList=new List<ICommandBuilder>();
            List<ITypeToken> tokens=new List<ITypeToken>();

            tokens.Add(token_);
            tokens.Add(_miniFactory.Gap());
            if (param_ != null)
            {
                tokens.Add(param_);
                tokens.Add(_miniFactory.Gap());
           }
            buildersList.Add(BuildNew(tokens, this._miniFactory.EmptyString()));
            this._commandBuilder=BuildNew(buildersList, this._miniFactory.Gap());
       }
        public void ParametrizedCommand(List<ITypeToken> tokens_,ITypeToken delimeter_=null)
        {
            List<ICommandBuilder> buildersList=new List<ICommandBuilder>();
            ITypeToken placeholder=null;

            if (delimeter_==null) {placeholder=this._miniFactory.EmptyString();}
            else {placeholder=delimeter_;}

            buildersList.Add(BuildNew(tokens_, placeholder));
            this._commandBuilder=BuildNew(buildersList, this._miniFactory.Gap());
       }

        /// <summary>
        /// For creating commans with nesting functionality and double indenting (Nest).
        /// Adds to the left from all previous commandbuilder
        /// </summary>
        /// <param name="tokenList">List of tokens added to the left</param>
        /// <param name="format_">Format for token assembly. If null standart with gap generated</param>
        /// <returns></returns>
        public ICommandBuilder CommandFormattedRebuild(List<ITypeToken> tokenList, ITypeToken format_)
        {
            ReBuild(tokenList, format_);
            return this._commandBuilder;
       }
        /// <summary>
        /// For creating commans with nesting functionality and left indenting (Content,Create,Where).
        /// Adds to the right from all previous commandbuilder but with parameters
        /// </summary>
        /// <param name="token_">Command token</param>
        /// <param name="builder_">Builder, to which Tokens command is used</param>
        /// <param name="gapBefore">Gap before command presence</param>
        /// <returns></returns>
        public ICommandBuilder ParametrizedCommand(ITypeToken token_, ICommandBuilder builder_, bool gapBefore)
        {
           
            List<ITypeToken> tokenList=new List<ITypeToken>();
            if (gapBefore)
            {
                tokenList.Add(_miniFactory.Gap());
           }
            tokenList.Add(token_);

            if (builder_ != null)
            {
                tokenList.Add(_miniFactory.Gap());
                tokenList.AddRange(builder_.Tokens);
           }

            Build(tokenList, _formatGenerator.FromatFromTokenArray(tokenList, _miniFactory.EmptyString()));
            return this._commandBuilder;
       }
        /// <summary>
        /// For creating commans with nesting functionality and left indenting (Select).
        /// Adds to the left from all previous commandbuilder. Has no left gap string
        /// </summary>
        /// <param name="token_">Command token</param>
        /// <param name="params_">Command parameters</param>
        /// <param name="builder_">Builder, to which Tokens command is used</param>
        /// <param name="gapBefore">Gap before command presence</param>
        /// <returns></returns>
        public ICommandBuilder ParametrizedCommandRebuild(ITypeToken token_
            , ICommandBuilder params_, ICommandBuilder builder_,bool gapBefore)
        {
            List<ITypeToken> tokenList=new List<ITypeToken>();
            if (gapBefore)
            {
                tokenList.Add(_miniFactory.Gap());
           }
            tokenList.Add(token_);

            if (params_ != null)
            {
                tokenList.Add(_miniFactory.Gap());
                tokenList.AddRange(params_.Tokens);
           }

            if(builder_!=null)
            {
                if(builder_.Tokens !=null)
                {
                    tokenList.Add(_miniFactory.Gap());
                    tokenList.AddRange(builder_.Tokens);                   
               }
           }

            ReBuild(tokenList, _formatGenerator.FromatFromTokenArray(tokenList, _miniFactory.EmptyString()));
            return this._commandBuilder;
       }

        
        //<<<obsolette possible not used
        public ICommandBuilder CommandFormatted(List<ITypeToken> tokenList, ITypeToken format_)
        {
            Build(tokenList, format_);
            return this._commandBuilder;
        }


        [Obsolete]
        public void Build(List<ITypeToken> tokenList_, IFormatFromListGenerator formatgenerator_)
        {
            tokenList_.Add(_miniFactory.EmptyString());
            this._commandBuilder.AddTokens(tokenList_);

            //generate format from token list with system.empty placeholder List<n> => "{0} ..{}.. {n}"
            ITypeToken token=_miniFactory.NewToken();
            token=formatgenerator_.FromatFromTokenArray(tokenList_);

            this._commandBuilder.AddFormat(token);
            lastGeneneratedCommand=this._commandBuilder.Build().GetText();
        }        
        /// <summary>
        /// Universal builder of tokens, looks like reinvention of concat, but for classes
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        [Obsolete]
        public ICommandBuilder Command(List<ITypeToken> tokenList_, ITypeToken format=null)
        {
            ITypeToken token=_miniFactory.NewToken();
            if (format==null)
            {
                //generate format from token list with system.empty placeholder List<n> => "{0} ..{}.. {n}"
                token=_formatGenerator.FromatFromTokenArray(tokenList_);
            }
            else {token=format;}

            this._commandBuilder.AddTokens(tokenList_);
            this._commandBuilder.AddFormat(token);
            this._commandBuilder.Build();
            return this._commandBuilder;
        }
        /// <summary>
        /// Universal builder of commands use for large commands aggregation
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        [Obsolete]
        public ICommandBuilder Command(List<ICommandBuilder> tokens, ITypeToken format)
        {
            _commandBuilder.BindBuilders(tokens, format);
            return this._commandBuilder;
        }
        [Obsolete]
        public List<ITypeToken> Tokenise(List<ITypeToken> token_, ITypeToken param=null)
        {
            if (param != null)
            {
                token_.Add(param);
           }

            return token_;
        }
        [Obsolete]
        public List<ITypeToken> Tokenise(ITypeToken token_, ICommandBuilder param)
        {
            List<ITypeToken> tokenList=new List<ITypeToken>();

            tokenList.Add(token_);
            if (param != null)
            {
                tokenList.AddRange(param.Tokens);
           }
            return tokenList;
        }

        public ICommandBuilder Nest(ICommandBuilder param, ITypeToken leftToken_, ITypeToken rightToken_
          , ITypeToken format=null)
        {
            List<ICommandBuilder> commands_=new List<ICommandBuilder>();
            ITypeToken lt, rt;

            lt=(leftToken_==null) ? _queryFactory.LeftRoundBraket() : leftToken_;
            rt=(rightToken_==null) ? _queryFactory.RightRoundBraket() : rightToken_;

            List<ITypeToken> ltL=new List<ITypeToken> {lt};
            ITypeToken t1=_formatFactory.FormatGenerator(_miniFactory).FromatFromTokenArray(ltL);
            commands_.Add(_commandFactory.CommandBuilder(_miniFactory, _formatFactory, ltL, t1));

            if (param.Tokens != null || param.Text!=null)
            {
                commands_.Add(param);
           }

            List<ITypeToken> ltR=new List<ITypeToken> {rt};
            ITypeToken t2=_formatFactory.FormatGenerator(_miniFactory).FromatFromTokenArray(ltL);
            commands_.Add(_commandFactory.CommandBuilder(_miniFactory, _formatFactory, ltR, t2));

            this._commandBuilder.BindBuilders(
                commands_, _formatFactory.FormatGenerator(_miniFactory).FromatFromTokenArray(commands_,_miniFactory.EmptyString())
                );

            return GetBuilder();
            
        }

     
        public ICommandBuilder Content(ICommandBuilder param_)
        {
            List<ITypeToken> tokens_=new List<ITypeToken>();
            List<ICommandBuilder> builders_=new List<ICommandBuilder>();

            tokens_.Add(_queryFactory.ContentToken());
            tokens_.Add(_miniFactory.Gap());

            builders_.Add(
            _commandFactory.CommandBuilder(_miniFactory, _formatFactory
                , tokens_, _formatGenerator.FromatFromTokenArray(tokens_, _miniFactory.EmptyString()))
                );

            if (param_ != null)
            {
                builders_.Add(param_);
                tokens_=new List<ITypeToken>();
                tokens_.Add(_miniFactory.Gap());
                builders_.Add(
               _commandFactory.CommandBuilder(_miniFactory, _formatFactory
                   , tokens_, _formatGenerator.FromatFromTokenArray(tokens_, _miniFactory.EmptyString())).Build()
                   );
            }
          
            this._commandBuilder=BuildNew(builders_, this._miniFactory.EmptyString());
            return GetBuilder();          
        }
        public ICommandBuilder Content(ITypeToken param_)
        {
            List<ITypeToken> tokens_ = new List<ITypeToken>();
            List<ICommandBuilder> builders_ = new List<ICommandBuilder>();

            tokens_.Add(_miniFactory.Gap());
            tokens_.Add(_queryFactory.ContentToken());
           
            if (param_ != null)
            {               
                tokens_.Add(_miniFactory.Gap());
                tokens_.Add(param_);
                            
            }

            builders_.Add(
            _commandFactory.CommandBuilder(_miniFactory, _formatFactory
                , tokens_, _formatGenerator.FromatFromTokenArray(tokens_, _miniFactory.EmptyString()))
                );

            this._commandBuilder = BuildNew(builders_, this._miniFactory.EmptyString());
            return GetBuilder();
        }
        public ICommandBuilder Extends(ITypeToken param)
        {
            List<ITypeToken> tokenList=new List<ITypeToken>();
            tokenList.Add(_miniFactory.Gap());
            tokenList.Add(_queryFactory.ExtendsToken());
            if (param != null)
            {
                tokenList.Add(_miniFactory.Gap());
                tokenList.Add(param);
           }

            ParametrizedCommand(tokenList, null);
            return GetBuilder();
            return GetBuilder();
       }
        public ICommandBuilder Class(ITypeToken param=null)
        {
            List<ITypeToken> tokenList=new List<ITypeToken>();
            tokenList.Add(_miniFactory.Gap());
            tokenList.Add(_queryFactory.ClassToken());
            if (param != null)
            {
                tokenList.Add(_miniFactory.Gap());
                tokenList.Add(param);
           }

            ParametrizedCommand(tokenList, null);
            return GetBuilder();           
        }
        public ICommandBuilder ClassCheck(ITypeToken param)
        {
            List<ITypeToken> tokenList=new List<ITypeToken>();
            tokenList.Add(_queryFactory.Dog());
            tokenList.Add(_queryFactory.ClassToken());
            tokenList.Add(_queryFactory.LeftSquareBraket());
            tokenList.Add(_miniFactory.NewToken("0"));
            tokenList.Add(_queryFactory.RightSquareBraket());
            tokenList.Add(_queryFactory.Equals());
            tokenList.Add(_miniFactory.NewToken("'"));
            
            tokenList.Add(param);

            tokenList.Add(_miniFactory.NewToken("'"));

            ParametrizedCommand(tokenList, null);
            return GetBuilder();           
        }

        public ICommandBuilder Property(ITypeToken class_, ITypeToken prop_, ITypeToken type_
        , ITypeToken mandatory_, ITypeToken notnull_)
        {
            List<ICommandBuilder> builders=new List<ICommandBuilder>();

            builders.Add(PropertyItem(class_, prop_));
            builders.Add(PropertyType(type_));
            if (mandatory_ != null && notnull_ != null)
            {
                builders.Add(PropertyCondition(mandatory_, notnull_));
           }

            return BuildNew(builders, _miniFactory.EmptyString());
        }

        public ICommandBuilder PropertyItem(ITypeToken class_,ITypeToken prop_ )
        {
            List<ITypeToken> tokenList=new List<ITypeToken>();

            tokenList.Add(_miniFactory.Gap());
            tokenList.Add(_queryFactory.PropertyToken());
            tokenList.Add(_miniFactory.Gap());
            tokenList.Add(class_);
            tokenList.Add(_miniFactory.Dot());
            tokenList.Add(prop_);
            ParametrizedCommand(tokenList,null);
            return GetBuilder();           
        }
        public ICommandBuilder PropertyType(ITypeToken type_)
        {
            List<ITypeToken> tokenList=new List<ITypeToken>();

            tokenList.Add(_miniFactory.Gap());
            tokenList.Add(type_);           

            ParametrizedCommand(tokenList, null);
            return GetBuilder();
        }
        public ICommandBuilder PropertyCondition(ITypeToken mandatory_, ITypeToken notnull_)
        {
            List<ITypeToken> tokenList=new List<ITypeToken>();

            tokenList.Add(_miniFactory.Gap());
            tokenList.Add(_queryFactory.LeftRoundBraket());
            tokenList.Add(_queryFactory.Mandatory());
            tokenList.Add(_miniFactory.Gap());
            tokenList.Add(mandatory_);
            tokenList.Add(_miniFactory.Coma());
            tokenList.Add(_queryFactory.NotNull());
            tokenList.Add(_miniFactory.Gap());
            tokenList.Add(notnull_);
            tokenList.Add(_queryFactory.RightRoundBraket());
          
            ParametrizedCommand(tokenList, null);
            return GetBuilder();
        }

        public ICommandBuilder Vertex(ITypeToken param=null)
        {
            List<ITypeToken> tokenList=new List<ITypeToken>();
            tokenList.Add(_miniFactory.Gap());
            tokenList.Add(_queryFactory.VertexToken());
            if(param!=null)
            {
                tokenList.Add(_miniFactory.Gap());
                tokenList.Add(param);
            }

            ParametrizedCommand(tokenList,null);
            return GetBuilder();
        }
        public ICommandBuilder Edge(ITypeToken param=null)
        {
            List<ITypeToken> tokenList = new List<ITypeToken>();

            tokenList.Add(_miniFactory.Gap());
            tokenList.Add(_queryFactory.EdgeToken());

            if (param != null)
            {
                tokenList.Add(_miniFactory.Gap());
                tokenList.Add(param);
            }

            ParametrizedCommand(tokenList, null);
            return GetBuilder();
        }

        public ICommandBuilder Create(ITypeToken param_=null)
        {
            List<ITypeToken> tokens_=new List<ITypeToken>();

            tokens_.Add(_queryFactory.CreateToken());            
            if (param_ != null)
            {
                tokens_.Add(_miniFactory.Gap());
                tokens_.Add(param_);              
            }

            ParametrizedCommand(tokens_, null);
            return GetBuilder();
        }

        /// <summary>
        /// Returns select from command when null passed. when any commandbuilderpassed returns select {0} from command
        /// </summary>
        /// <param name="param_">Command builder containining what to select </param>
        /// <returns></returns>
        public ICommandBuilder Select(ICommandBuilder param_=null)
        {
            List<ITypeToken> tokens_=new List<ITypeToken>();
            List<ICommandBuilder> builders_= new List<ICommandBuilder>();

            //add select tokens
            tokens_.Add(_queryFactory.SelectToken());
            tokens_.Add(_miniFactory.Gap());

            //build select command builder
            builders_.Add(
            _commandFactory.CommandBuilder(_miniFactory, _formatFactory
                , tokens_, _formatGenerator.FromatFromTokenArray(tokens_, _miniFactory.EmptyString()))
            );

            //add all other tokens after select
            if (param_ != null)
            {
                builders_.Add(param_);
                tokens_=new List<ITypeToken>();
                tokens_.Add(_miniFactory.Gap());
                builders_.Add(
               _commandFactory.CommandBuilder(_miniFactory, _formatFactory
                   , tokens_, _formatGenerator.FromatFromTokenArray(tokens_, _miniFactory.EmptyString())).Build()
                   );
            }

            this._commandBuilder=BuildNew(builders_, this._miniFactory.EmptyString());
            return GetBuilder();

        }       
        public ICommandBuilder Select(List<ITypeToken> param_)
        {
            List<ITypeToken> tokens_=new List<ITypeToken>();

            tokens_.Add(_queryFactory.SelectToken());
            tokens_.Add(_miniFactory.Gap());
            if (param_ != null)
            {
                tokens_.AddRange(param_);               
            }
            
            ParametrizedCommand(tokens_, null);
            return GetBuilder();

        }
        public ICommandBuilder Traverse(List<ITypeToken> param_=null)
        {
            List<ITypeToken> tokens_=new List<ITypeToken>();

            tokens_.Add(_queryFactory.TraverseToken());
            tokens_.Add(_miniFactory.Gap());
            if (param_ != null)
            {
                tokens_.AddRange(param_);               
            }
            
            ParametrizedCommand(tokens_, null);
            return GetBuilder();

        }

        public ICommandBuilder And(ITypeToken param_=null)
        {
            List<ITypeToken> tokens_=new List<ITypeToken>();
            tokens_.Add(_miniFactory.Gap());
            tokens_.Add(_queryFactory.AndToken()); 
            
            if (param_ != null)
            {
                tokens_.Add(_miniFactory.Gap());
                tokens_.Add(param_);
            }

            ParametrizedCommand(tokens_, null);
            return GetBuilder();

        }
        public ICommandBuilder From(ITypeToken param_=null)
        {
            List<ITypeToken> tokens_=new List<ITypeToken>();
            tokens_.Add(_miniFactory.Gap());
            tokens_.Add(_queryFactory.FromToken()); 
            
            if (param_ != null)
            {
                tokens_.Add(_miniFactory.Gap());
                tokens_.Add(param_);
            }

            ParametrizedCommand(tokens_, null);
            return GetBuilder();

        }
        public ICommandBuilder Where(ICommandBuilder param_=null)
        {
            List<ITypeToken> tokens_=new List<ITypeToken>();
            List<ICommandBuilder> builders_=new List<ICommandBuilder>();

            if (param_ != null)
            {
                tokens_.Add(_miniFactory.Gap());
                tokens_.Add(_queryFactory.WhereToken());

                builders_.Add(
                _commandFactory.CommandBuilder(_miniFactory, _formatFactory
                    , tokens_, _formatGenerator.FromatFromTokenArray(tokens_, _miniFactory.EmptyString()))
                    );

                tokens_.Clear();
                tokens_.Add(_miniFactory.Gap());
                builders_.Add(
                _commandFactory.CommandBuilder(_miniFactory, _formatFactory
                , tokens_, _formatGenerator.FromatFromTokenArray(tokens_, _miniFactory.EmptyString()))
                );
                
                builders_.Add(param_);
           }

            this._commandBuilder=BuildNew(builders_, this._miniFactory.EmptyString());
            return GetBuilder();
        }
        public ICommandBuilder Where(ITypeToken param_=null)
        {
            List<ITypeToken> tokens_=new List<ITypeToken>();
            tokens_.Add(_miniFactory.Gap());
            tokens_.Add(_queryFactory.WhereToken()); 
            
            if (param_ != null)
            {
                tokens_.Add(_miniFactory.Gap());
                tokens_.Add(param_);
            }

            ParametrizedCommand(tokens_, null);
            return GetBuilder();

        }

        public ICommandBuilder Between(ITypeToken param_=null)
        {
            List<ITypeToken> tokens_=new List<ITypeToken>();
            tokens_.Add(_miniFactory.Gap());
            tokens_.Add(_queryFactory.Between()); 
            
            if (param_ != null)
            {
                tokens_.Add(_miniFactory.Gap());
                tokens_.Add(param_);
            }

            ParametrizedCommand(tokens_, null);
            return GetBuilder();

        }

        public ICommandBuilder To(ITypeToken param_=null)
        {
            List<ITypeToken> tokens_=new List<ITypeToken>();
            tokens_.Add(_miniFactory.Gap());
            tokens_.Add(_queryFactory.ToToken());

            if (param_ != null)
            {
                tokens_.Add(_miniFactory.Gap());
                tokens_.Add(param_);
            }

            ParametrizedCommand(tokens_, null);
            return GetBuilder();
        }
        public ICommandBuilder Delete(ITypeToken param_ = null)
        {
            List<ITypeToken> tokens_ = new List<ITypeToken>();
                        
            tokens_.Add(_queryFactory.DeleteToken());

            if (param_ != null)
            {
                tokens_.Add(_miniFactory.Gap());
                tokens_.Add(param_);
            }

            ParametrizedCommand(tokens_, null);
            return GetBuilder();
        }

        public ICommandBuilder InVformat(ITypeToken dir_,ITypeToken type_,ITypeToken param_)
        {
            List<ITypeToken> tokens_ = new List<ITypeToken>();
                 
            tokens_.Add(dir_);
            if(type_!=null){
              tokens_.Add(type_);
            }
            tokens_.Add(_queryFactory.LeftRoundBraket());
          
            if(param_!=null){
              tokens_.Add(_miniFactory.Apostrophe());
              tokens_.Add(param_);
              tokens_.Add(_miniFactory.Apostrophe());
            }
            
            tokens_.Add(_queryFactory.RightRoundBraket());

            ParametrizedCommand(tokens_, null);
            return GetBuilder();
        }
        public ICommandBuilder OutV(ITypeToken param_=null)
        {
            return InVformat(_queryFactory.Out(),_queryFactory.V(),param_);
        }
        public ICommandBuilder OutE(ITypeToken param_=null)
        {
            return InVformat(_queryFactory.Out(),_queryFactory.E(),param_);
        }
        public ICommandBuilder InV(ITypeToken param_=null)
        {
            return InVformat(_queryFactory.In(),_queryFactory.V(),param_);
        }
        public ICommandBuilder InE(ITypeToken param_=null)
        {
            return InVformat(_queryFactory.In(),_queryFactory.E(),param_);
        }
        public ICommandBuilder In(ITypeToken param_=null)
        {
            return InVformat(_queryFactory.In(),null,param_);
        }
        public ICommandBuilder Out(ITypeToken param_=null)
        {
            return InVformat(_queryFactory.Out(),null,param_);
        }

        public ICommandBuilder As(ITypeToken aliace_)
        {
            List<ITypeToken> tokens_ = new List<ITypeToken>();

            tokens_.Add(_miniFactory.Gap());
            tokens_.Add(_queryFactory.As());
            tokens_.Add(aliace_);

            ParametrizedCommand(tokens_, null);
            return GetBuilder();
        }

        public ICommandBuilder Expand(ICommandBuilder param_,ITypeToken alias)
        {
            List<ITypeToken> tokens_ = new List<ITypeToken>();
            List<ICommandBuilder> builders_ = new List<ICommandBuilder>();

            //add expand tokens before all
            tokens_.Add(_queryFactory.SelectToken());
            tokens_.Add(_miniFactory.Gap());
            tokens_.Add(_queryFactory.Expand());
            tokens_.Add(_queryFactory.LeftRoundBraket());
            tokens_.Add(alias);
            tokens_.Add(_queryFactory.RightRoundBraket());
            tokens_.Add(_miniFactory.Gap());
            tokens_.Add(_queryFactory.FromToken());
            tokens_.Add(_queryFactory.LeftRoundBraket());

            //build expand command builder
            builders_.Add(
            _commandFactory.CommandBuilder(_miniFactory, _formatFactory
                , tokens_, _formatGenerator.FromatFromTokenArray(tokens_, _miniFactory.EmptyString()))
            );

            //add all other tokens after select            
            builders_.Add(param_);
            tokens_ = new List<ITypeToken>();
            tokens_.Add(_miniFactory.Gap());
            builders_.Add(
            _commandFactory.CommandBuilder(_miniFactory, _formatFactory
                , tokens_, _formatGenerator.FromatFromTokenArray(tokens_, _miniFactory.EmptyString())).Build()
                );

            tokens_.Clear();
            tokens_.Add(_queryFactory.RightRoundBraket());

            //build expand command builder
            builders_.Add(
            _commandFactory.CommandBuilder(_miniFactory, _formatFactory
                , tokens_, _formatGenerator.FromatFromTokenArray(tokens_, _miniFactory.EmptyString()))
            );

            this._commandBuilder = BuildNew(builders_, this._miniFactory.EmptyString());
            return GetBuilder();


            ParametrizedCommand(tokens_, null);
            return GetBuilder();
        }

        public ICommandBuilder AlterPropertyUUID(ITypeToken class_,ITypeToken property_,ITypeToken orientFunct_)
        {
            List<ITypeToken> tokens_ = new List<ITypeToken>();

            //add expand tokens before all
            tokens_.Add(_queryFactory.AlterToken());
            tokens_.Add(_miniFactory.Gap());
            tokens_.Add(_queryFactory.PropertyToken());
            tokens_.Add(_miniFactory.Gap());

            tokens_.Add(class_);
            tokens_.Add(_miniFactory.Dot());
            tokens_.Add(property_);

            tokens_.Add(_miniFactory.Gap());         
            tokens_.Add(_queryFactory.DefaultToken());           
            tokens_.Add(_miniFactory.Gap());

            tokens_.Add(_queryFactory.DoubleQuotes());
            tokens_.Add(orientFunct_);
            tokens_.Add(_queryFactory.DoubleQuotes());

            ParametrizedCommand(tokens_, null);
            return GetBuilder();
        }

        public ICommandBuilder Update(ITypeToken param_ = null)
        {
            List<ITypeToken> tokens_ = new List<ITypeToken>();

            tokens_.Add(_queryFactory.UpdateToken());
            if (param_ != null)
            {
                tokens_.Add(_miniFactory.Gap());
                tokens_.Add(param_);
            }

            ParametrizedCommand(tokens_, null);
            return GetBuilder();
        }

    }

    /// <summary>
    /// Shemas for Orient URLs
    /// </summary>
    public class UrlShemasExplicit : Shemas
    {
      
        internal IOrientBodyFactory _QueryFactory;

        internal ITypeToken dbHost;

        public UrlShemasExplicit(ICommandBuilder commandBuilder_,
            IFormatFromListGenerator formatGenerator_,
            ITokenMiniFactory miniFactory_,
            IOrientBodyFactory queryFactory_)
            : base(commandBuilder_, formatGenerator_, miniFactory_)
        {
            _commandBuilder=commandBuilder_;
            _formatGenerator=formatGenerator_;
            _miniFactory=miniFactory_;
            _QueryFactory=queryFactory_;           
       }

        public UrlShemasExplicit(
          ICommandFactory commandFactory_
          , IFormatFactory formatFactory_
          , ITokenMiniFactory miniFactory_
            , IOrientBodyFactory queryFactory_)
            : base(commandFactory_,formatFactory_,miniFactory_)
        {
            _QueryFactory=queryFactory_;
       }

        public void AddHost(string host_)
        {
            this.dbHost=_miniFactory.NewToken(host_);
       }
        public ITypeToken GetHost()
        {
            return this.dbHost;
       }
        internal void ChekHost()
        {
            if (this.dbHost==null || this.dbHost.Text==null)
            {
                throw new Exception("No db host URL passed");
           }
       }
        public void Build(List<ITypeToken> tokenList_, ITypeToken format=null)
        {
            //tokenList_.Add(factory.NewEmptyString());            
            this._commandBuilder.AddTokens(tokenList_);

            //generate format from token list with system.empty placeholder List<n> => "{0} ..{}.. {n}"
            ITypeToken token=_miniFactory.NewToken();
            if (format==null)
            {
                token=_formatGenerator.FromatFromTokenArray(tokenList_);
           }
            else {token=format;}

            this._commandBuilder.AddFormat(token);          
       }
        public void ReBuild(List<ITypeToken> tokenList_, ITypeToken format=null)
        {
            //tokenList_.Add(factory.NewEmptyString());            
            this._commandBuilder.BindTokens(tokenList_);

            //generate format from token list with system.empty placeholder List<n> => "{0} ..{}.. {n}"
            ITypeToken token=_miniFactory.NewToken();
            if (format==null)
            {
                token=_formatGenerator.FromatFromTokenArray(tokenList_);
           }
            else {token=format;}

            this._commandBuilder.BindFormat(token);
            lastGeneneratedCommand=this._commandBuilder.Build().GetText();
       }
        public void ReBuildDelimeter(List<ITypeToken> tokenList_, ITypeToken delimeter_)
        {
            //tokenList_.Add(factory.NewEmptyString());            
            this._commandBuilder.BindTokens(tokenList_);

            //generate format from token list with system.empty placeholder List<n> => "{0} ..{}.. {n}"
            ITypeToken token=_miniFactory.NewToken();
            if (delimeter_==null)
            {
                token=_formatGenerator.FromatFromTokenArray(tokenList_);
            }
            else {
                token=_formatGenerator.FromatFromTokenArray(tokenList_, delimeter_);
           }

            this._commandBuilder.BindFormat(token);
            lastGeneneratedCommand=this._commandBuilder.Build().GetText();
       }


        public ICommandBuilder Database(ITypeToken databaseName_)
        {
            ChekHost();
            List<ITypeToken> typeToken=new List<ITypeToken>();
            typeToken.Add(this.dbHost);

            typeToken.Add(this._QueryFactory.BackSlash());
            typeToken.Add(this._QueryFactory.Database());

            typeToken.Add(this._QueryFactory.BackSlash());
            typeToken.Add(databaseName_);

            typeToken.Add(this._QueryFactory.BackSlash());
            typeToken.Add(this._QueryFactory.PLocal());

            ReBuildDelimeter(typeToken, new TextToken() {Text=string.Empty});
            return this._commandBuilder;
       }
        public ICommandBuilder Connect(ITypeToken databaseName_)
        {
            ChekHost();
            List<ITypeToken> typeToken=new List<ITypeToken>();
            typeToken.Add(this.dbHost);

            typeToken.Add(this._QueryFactory.BackSlash());
            typeToken.Add(this._QueryFactory.Connect());

            typeToken.Add(this._QueryFactory.BackSlash());
            typeToken.Add(databaseName_);

            ReBuildDelimeter(typeToken, new TextToken() {Text=string.Empty});
            return this._commandBuilder;
       }

        public ICommandBuilder Command(ITypeToken databaseName_)
        {
            ChekHost();
            List<ITypeToken> typeToken=new List<ITypeToken>();
            typeToken.Add(this.dbHost);

            typeToken.Add(this._QueryFactory.BackSlash());
            typeToken.Add(this._QueryFactory.Command());

            typeToken.Add(this._QueryFactory.BackSlash());
            typeToken.Add(databaseName_);

            typeToken.Add(this._QueryFactory.BackSlash());
            typeToken.Add(this._QueryFactory.sql());

            ReBuildDelimeter(typeToken, _miniFactory.EmptyString());
            return this._commandBuilder;
       }
        public ICommandBuilder Batch(ITypeToken databaseName_)
        {
            ChekHost();
            List<ITypeToken> typeToken=new List<ITypeToken>();
            typeToken.Add(this.dbHost);

            typeToken.Add(this._QueryFactory.BackSlash());
            typeToken.Add(this._QueryFactory.Batch());

            typeToken.Add(this._QueryFactory.BackSlash());
            typeToken.Add(databaseName_);

            typeToken.Add(this._QueryFactory.BackSlash());
            typeToken.Add(this._QueryFactory.sql());

            ReBuildDelimeter(typeToken, new TextToken() {Text= string.Empty});
            return this._commandBuilder;
       }

    }
    /// <summary>
    /// Shemas for Orient request body parameters command\batch
    /// </summary>
    public class BodyShemas : UrlShemasExplicit
    {

        public BodyShemas(
        ICommandFactory commandFactory_
        , IFormatFactory formatFactory_
        , ITokenMiniFactory miniFactory_
        , IOrientBodyFactory queryBoduFactory_)
        : base(commandFactory_, formatFactory_, miniFactory_, queryBoduFactory_)
        {
            
       }    

        public ICommandBuilder Command(ICommandBuilder command_)
        {            
            List<ITypeToken> typeToken=new List<ITypeToken>();
            List<ICommandBuilder> commandBuilers=new List<ICommandBuilder>();

            typeToken.Add(this._QueryFactory.LeftFgGap());
            typeToken.Add(this._QueryFactory.Quotes());
            typeToken.Add(this._QueryFactory.Command());
            typeToken.Add(this._QueryFactory.Quotes());
            typeToken.Add(this._QueryFactory.Colon());

           
            //typeToken.Add(this._QueryFactory.Slash());
            typeToken.Add(this._QueryFactory.Quotes());

            //build left part of body with no gaps and add to builder list           
            commandBuilers.Add(BuildNew(typeToken, _miniFactory.EmptyString()));
            
            commandBuilers.Add(BuildFormatNew(command_.Tokens, command_.FormatPattern));

            typeToken=new List<ITypeToken>();

            //typeToken.Add(this._QueryFactory.Slash());
            typeToken.Add(this._QueryFactory.Quotes());
            typeToken.Add(this._QueryFactory.RightFgGap());

            commandBuilers.Add(BuildNew(typeToken, _miniFactory.EmptyString()));

            BuildNew(commandBuilers, _miniFactory.EmptyString());               
            return GetBuilder();
       }

        public ICommandBuilder SelectCommand(ICommandBuilder command_)
        {
            List<ITypeToken> typeToken = new List<ITypeToken>();
            List<ICommandBuilder> commandBuilers = new List<ICommandBuilder>();

            typeToken.Add(this._QueryFactory.LeftFgGap());
           
            typeToken.Add(this._QueryFactory.Quotes());
            typeToken.Add(this._QueryFactory.Command());
          
            typeToken.Add(this._QueryFactory.Quotes());
            typeToken.Add(this._QueryFactory.Colon());


            typeToken.Add(this._QueryFactory.Slash());
            typeToken.Add(this._QueryFactory.Quotes());

            //build left part of body with no gaps and add to builder list           
            commandBuilers.Add(BuildNew(typeToken, _miniFactory.EmptyString()));

            commandBuilers.Add(BuildFormatNew(command_.Tokens, command_.FormatPattern));

            typeToken = new List<ITypeToken>();

            typeToken.Add(this._QueryFactory.Slash());
            typeToken.Add(this._QueryFactory.Quotes());
            typeToken.Add(this._QueryFactory.RightFgGap());

            commandBuilers.Add(BuildNew(typeToken, _miniFactory.EmptyString()));

            BuildNew(commandBuilers, _miniFactory.EmptyString());
            return GetBuilder();
        }

        public ICommandBuilder Batch(ICommandBuilder command_)
        {           
            List<ITypeToken> typeToken=new List<ITypeToken>();
            List<ICommandBuilder> commandBuilers=new List<ICommandBuilder>();
            
            //{
            typeToken.Add(this._QueryFactory.LeftFgGap());

            typeToken.Add(this._QueryFactory.Quotes());
            typeToken.Add(this._QueryFactory.Transactions());
            typeToken.Add(this._QueryFactory.Quotes());

            //:
            typeToken.Add(this._QueryFactory.Colon());

            typeToken.Add(this._QueryFactory.True());
            typeToken.Add(this._QueryFactory.Comma());

            typeToken.Add(this._QueryFactory.Quotes());
            typeToken.Add(this._QueryFactory.Operations());
            typeToken.Add(this._QueryFactory.Quotes());

            //:
            typeToken.Add(this._QueryFactory.Colon());
                
                //{[
                typeToken.Add(this._QueryFactory.LeftSqGap());
                typeToken.Add(this._QueryFactory.LeftFgGap());

                    typeToken.Add(this._QueryFactory.Quotes());
                    typeToken.Add(this._QueryFactory.Type());
                    typeToken.Add(this._QueryFactory.Quotes());
                    //:
                    typeToken.Add(this._QueryFactory.Colon());

                    typeToken.Add(this._QueryFactory.Quotes());
                    typeToken.Add(this._QueryFactory.Sctipt());
                    typeToken.Add(this._QueryFactory.Quotes());
                    typeToken.Add(this._QueryFactory.Comma());


                    typeToken.Add(this._QueryFactory.Quotes());
                    typeToken.Add(this._QueryFactory.Language());
                    typeToken.Add(this._QueryFactory.Quotes());
                    //:
                    typeToken.Add(this._QueryFactory.Colon());

                    typeToken.Add(this._QueryFactory.Quotes());
                    typeToken.Add(this._QueryFactory.sql());
                    typeToken.Add(this._QueryFactory.Quotes());
                    typeToken.Add(this._QueryFactory.Comma());

                        typeToken.Add(this._QueryFactory.Quotes());
                        typeToken.Add(this._QueryFactory.Sctipt());
                        typeToken.Add(this._QueryFactory.Quotes());
                        //:
                        typeToken.Add(this._QueryFactory.Colon());
                        
                        //["
                        typeToken.Add(this._QueryFactory.LeftSqGap());
                        typeToken.Add(this._QueryFactory.Quotes());

            //build left part of body with no gaps and add to builder list
            commandBuilers.Add(BuildNew(typeToken, _miniFactory.EmptyString()));

            //build command preserving with command format pattern gaps and add to builder list
            commandBuilers.Add(
        BuildFormatNew(command_.Tokens, command_.FormatPattern)
    );

    typeToken=new List<ITypeToken>();

            //"]}]}
            typeToken.Add(this._QueryFactory.Quotes());
            typeToken.Add(this._QueryFactory.RightSqGap());
            typeToken.Add(this._QueryFactory.RightFgGap());
            typeToken.Add(this._QueryFactory.RightSqGap());            
            typeToken.Add(this._QueryFactory.RightFgGap());

    //build left part of body with no gaps and add to builder list   
    commandBuilers.Add(BuildNew(typeToken, _miniFactory.EmptyString()));

    BuildNew(commandBuilers, _miniFactory.Gap());
    return GetBuilder();
       }

    }
    
    #endregion


    /// <summary>
    /// Chains cmmand with parameters.
    /// </summary>
    public class CommandsChain
    {

        //contains basic syntax factory
        ITokenMiniFactory _tokenMiniFactory;
        //contains specific orient syntax factory
        IOrientQueryFactory _tokenOrientFactory;
        //format generators factory
        IFormatFactory _formatFactory;
        //commandBuilder factory
        ICommandFactory _commandFactory;

        //commandbuilder for chaining        
        ICommandBuilder _commandBuilder;

        List<ICommandBuilder> _commands;
        IFormatFromListGenerator _formatGenerator;
        
        CommandShemasExplicit _commandShemas;

        public CommandsChain(
            ITokenMiniFactory tokenMiniFactory_,
            IOrientQueryFactory tokenQueryFactory_,
            IFormatFactory formatFactory_,
            ICommandFactory commandFactory_)
        {
            
            this._tokenMiniFactory=tokenMiniFactory_;
            this._tokenOrientFactory=tokenQueryFactory_;
            this._formatFactory=formatFactory_;
            this._commandFactory=commandFactory_;

            this._commandBuilder=this._commandFactory.CommandBuilder(this._tokenMiniFactory, this._formatFactory);
            this._formatGenerator=_formatFactory.FormatGenerator(this._tokenMiniFactory);

            _commands=new List<ICommandBuilder>();
            
            _commandShemas=
                new CommandShemasExplicit(
                    this._commandFactory
                    ,this._formatFactory
                    ,this._tokenMiniFactory
                    ,this._tokenOrientFactory
                    );
        }

        public IFormatFromListGenerator GetGenerator()
        {
            return this._formatGenerator;
        }
        public string GetCommand()
        {
            if(this._commandBuilder.Text==null)
            {
                this._commandBuilder.Build();
            }
            return this._commandBuilder.Text.Text;
        }
        public ICommandBuilder GetBuilder()
        {
            return this._commandBuilder;
        }
              
        
        public CommandsChain NestSq()
        {
            this._commandBuilder=_commandShemas.Nest(this._commandBuilder, _tokenOrientFactory.LeftSquareBraket(), _tokenOrientFactory.RightSquareBraket());
            return this;
        }
        public CommandsChain NestRnd()
        {

            this._commandBuilder=_commandShemas.Nest(this._commandBuilder, _tokenOrientFactory.LeftRoundBraket(), _tokenOrientFactory.RightRoundBraket());
            return this;
        }
        public CommandsChain Nest(ITypeToken leftToken, ITypeToken rightToken)
        {
            this._commandBuilder=_commandShemas.Nest(this._commandBuilder, leftToken, rightToken);
            return this;
        }
        public CommandsChain Nest(ITypeToken leftToken_=null,ITypeToken rightToken_=null,ITypeToken format=null)
        {
            this._commands.Clear();
            this._commands.Add(this._commandShemas.Nest(this._commandBuilder, leftToken_, rightToken_, format));
            this._commandBuilder.BindBuilders(this._commands);
            this._commandBuilder.Build();
            return this;
        }

        public CommandsChain Where(ICommandBuilder param=null)
        {
            if (param != null)
            {
                this._commands=new List<ICommandBuilder>();

                if (this._commandBuilder.Tokens != null)
                {
                    this._commands.Add(this._commandBuilder);
                }
                    
                this._commands.Add(_commandShemas.Where(param));                  
            this._commandBuilder.BindBuilders(this._commands, this._formatGenerator.FromatFromTokenArray(this._commands, _tokenMiniFactory.Gap()));
            }
            return this;
        }        
        public CommandsChain Where(ITypeToken param_=null)
        {
            this._commands=new List<ICommandBuilder>();

            if (this._commandBuilder.Tokens != null)
            {
                this._commands.Add(this._commandBuilder);
            }

            this._commands.Add(_commandShemas.Where(param_));
            this._commandBuilder.BindBuilders(this._commands
                , this._formatGenerator.FromatFromTokenArray(this._commands,_tokenMiniFactory.EmptyString()));
            this._commandBuilder.Build();

            return this;
        }        

        public CommandsChain Between(ITypeToken param_=null)
        {
            this._commands=new List<ICommandBuilder>();

            if (this._commandBuilder.Tokens != null)
            {
                this._commands.Add(this._commandBuilder);
            }

            this._commands.Add(_commandShemas.Between(param_));

            this._commandBuilder.BindBuilders(this._commands
                , this._formatGenerator.FromatFromTokenArray(this._commands,_tokenMiniFactory.EmptyString()));
            this._commandBuilder.Build();

            return this;
        }

        public CommandsChain And(ITypeToken param_=null)
        {
            this._commands=new List<ICommandBuilder>();

            if (this._commandBuilder.Tokens != null)
            {
                this._commands.Add(this._commandBuilder);
            }

            this._commands.Add(_commandShemas.And(param_));

            this._commandBuilder.BindBuilders(this._commands
                , this._formatGenerator.FromatFromTokenArray(this._commands, _tokenMiniFactory.EmptyString()));
            this._commandBuilder.Build();

            return this;
        }

        public CommandsChain From(ITypeToken param_=null)
        {
            this._commands=new List<ICommandBuilder>();

            this._commands.Add(_commandShemas.From(param_));
           
            if (this._commandBuilder.Tokens != null)
            {
                this._commands.Add(this._commandBuilder);
            }

            this._commandBuilder.BindBuilders(this._commands, this._formatGenerator.FromatFromTokenArray(this._commands, _tokenMiniFactory.EmptyString()));
            this._commandBuilder.Build();
            return this;
        }
        public CommandsChain FromV(ITypeToken param_=null)
        {
            this._commands = new List<ICommandBuilder>();

            if (this._commandBuilder.Tokens != null)
            {
                this._commands.Add(this._commandBuilder);
            }
            if (param_ != null)
            {
                this._commands.Add(_commandShemas.From(param_));
            }
            this._commandBuilder.BindBuilders(this._commands, this._formatGenerator.FromatFromTokenArray(this._commands, _tokenMiniFactory.EmptyString()));
            this._commandBuilder.Build();
            return this;
        }
        
        public CommandsChain ToV(ITypeToken param_=null)
        {
            this._commands = new List<ICommandBuilder>();

            if (this._commandBuilder.Tokens != null)
            {
                this._commands.Add(this._commandBuilder);
            }
            if (param_ != null)
            {
                this._commands.Add(_commandShemas.To(param_));
            }
            this._commandBuilder.BindBuilders(this._commands, this._formatGenerator.FromatFromTokenArray(this._commands, _tokenMiniFactory.EmptyString()));
            this._commandBuilder.Build();
            return this;
        }

        public CommandsChain Select(ICommandBuilder columns_=null)
        {
            this._commands=new List<ICommandBuilder>();

            this._commands.Add( this._commandShemas.Select());
            this._commands.Add(columns_);
            if (this._commandBuilder.Tokens != null)
            {
                this._commands.Add(this._commandBuilder);
           }
            this._commandBuilder.BindBuilders(this._commands,this._formatGenerator.FromatFromTokenArray(this._commands,_tokenMiniFactory.EmptyString()));
            this._commandBuilder.Build();

            return this;
        }
        public CommandsChain Traverse(ICommandBuilder columns_=null)
        {
            this._commands=new List<ICommandBuilder>();

            this._commands.Add( this._commandShemas.Traverse());
            this._commands.Add(columns_);
            if (this._commandBuilder.Tokens != null)
            {
                this._commands.Add(this._commandBuilder);
           }
            this._commandBuilder.BindBuilders(this._commands,this._formatGenerator.FromatFromTokenArray(this._commands,_tokenMiniFactory.EmptyString()));
            this._commandBuilder.Build();

            return this;
        }
        
        public CommandsChain Create(ITypeToken param_=null)
        {

            this._commands=new List<ICommandBuilder>();

            if (this._commandBuilder.Tokens != null)
            {
                this._commands.Add(this._commandBuilder);
            }

            this._commands.Add(_commandShemas.Create(param_));

            this._commandBuilder.BindBuilders(this._commands
                , this._formatGenerator.FromatFromTokenArray(this._commands, _tokenMiniFactory.EmptyString()));
            this._commandBuilder.Build();

            return this;
        }
        public CommandsChain Class(ITypeToken param_=null)
        {
            this._commands=new List<ICommandBuilder>();

            if (this._commandBuilder.Tokens != null)
            {
                this._commands.Add(this._commandBuilder);
            }

            this._commands.Add(_commandShemas.Class(param_));

            this._commandBuilder.BindBuilders(this._commands
                , this._formatGenerator.FromatFromTokenArray(this._commands, _tokenMiniFactory.EmptyString()));
            this._commandBuilder.Build();
            return this;
        }
        public CommandsChain Vertex(ITypeToken param_=null)
        {
            this._commands=new List<ICommandBuilder>();

            if (this._commandBuilder.Tokens != null)
            {
                this._commands.Add(this._commandBuilder);
            }

            this._commands.Add(_commandShemas.Vertex(param_));

            this._commandBuilder.BindBuilders(this._commands, this._formatGenerator.FromatFromTokenArray(this._commands, _tokenMiniFactory.EmptyString()));
            this._commandBuilder.Build();
            return this;
        }
        public CommandsChain Extends(ITypeToken param_=null)
        {
            this._commands=new List<ICommandBuilder>();

            if (this._commandBuilder.Tokens != null)
            {
                this._commands.Add(this._commandBuilder);
            }

            this._commands.Add(_commandShemas.Extends(param_));

            this._commandBuilder.BindBuilders(this._commands, this._formatGenerator.FromatFromTokenArray(this._commands, _tokenMiniFactory.EmptyString()));
            this._commandBuilder.Build();
            return this;
        }

        public CommandsChain Edge(ITypeToken param_=null)
        {
            this._commands = new List<ICommandBuilder>();

            if (this._commandBuilder.Tokens != null)
            {
                this._commands.Add(this._commandBuilder);
            }

            this._commands.Add(_commandShemas.Edge(param_));

            this._commandBuilder.BindBuilders(this._commands, this._formatGenerator.FromatFromTokenArray(this._commands, _tokenMiniFactory.EmptyString()));
            this._commandBuilder.Build();
            return this;
        }

        public CommandsChain Content(ICommandBuilder param_=null)
        {
            this._commands=new List<ICommandBuilder>();

            if (this._commandBuilder.Tokens != null)
            {
                this._commands.Add(this._commandBuilder);
            }
            if (param_ != null)
            {
                this._commands.Add(_commandShemas.Content(param_));
            }
            this._commandBuilder.BindBuilders(this._commands);
            return this;
        }

        public CommandsChain Content(ITypeToken param_=null)
        {
            this._commands = new List<ICommandBuilder>();

            if (this._commandBuilder.Tokens != null)
            {
                this._commands.Add(this._commandBuilder);
            }
            if (param_ != null)
            {
                this._commands.Add(_commandShemas.Content(param_));
            }
            this._commandBuilder.BindBuilders(this._commands, this._formatGenerator.FromatFromTokenArray(this._commands, _tokenMiniFactory.EmptyString()));
            this._commandBuilder.Build();
            return this;
        }

        public CommandsChain Property(ITypeToken class_,ITypeToken property_,ITypeToken type_,ITypeToken mandatory_,ITypeToken notnull_)
        {
            this._commands=new List<ICommandBuilder>();

            if (this._commandBuilder.Tokens != null)
            {
                this._commands.Add(this._commandBuilder);
            }

            this._commands.Add(_commandShemas.Property(class_,property_,type_,mandatory_,notnull_));

            this._commandBuilder.BindBuilders(this._commands, this._formatGenerator.FromatFromTokenArray(this._commands, _tokenMiniFactory.EmptyString()));
            this._commandBuilder.Build();
            return this;
        }

        public CommandsChain Delete(ITypeToken param_=null)
        {
            this._commands = new List<ICommandBuilder>();

            if (this._commandBuilder.Tokens != null)
            {
                this._commands.Add(this._commandBuilder);
            }

            if (param_ != null && param_.Text != null)
            {
                this._commands.Add(_commandShemas.Delete(param_));
            }
            else {this._commands.Add(_commandShemas.Delete());}

            this._commandBuilder.BindBuilders(this._commands
                , this._formatGenerator.FromatFromTokenArray(this._commands, _tokenMiniFactory.EmptyString()));
            this._commandBuilder.Build();

            return this;
        }

        public CommandsChain InV(ITypeToken param_)
        {
            this._commands = new List<ICommandBuilder>();

            if (this._commandBuilder.Tokens != null)
            {
                this._commands.Add(this._commandBuilder);
            }

            this._commands.Add(_commandShemas.InV(param_));

            this._commandBuilder.BindBuilders(this._commands, this._formatGenerator.FromatFromTokenArray(this._commands, _tokenMiniFactory.EmptyString()));
            this._commandBuilder.Build();
            return this;
        }
        public CommandsChain InE(ITypeToken param_)
        {
            this._commands = new List<ICommandBuilder>();

            if (this._commandBuilder.Tokens != null)
            {
                this._commands.Add(this._commandBuilder);
            }

            this._commands.Add(_commandShemas.InE(param_));

            this._commandBuilder.BindBuilders(this._commands, this._formatGenerator.FromatFromTokenArray(this._commands, _tokenMiniFactory.EmptyString()));
            this._commandBuilder.Build();
            return this;
        }
        public CommandsChain OutV(ITypeToken param_)
        {
            this._commands = new List<ICommandBuilder>();

            if (this._commandBuilder.Tokens != null)
            {
                this._commands.Add(this._commandBuilder);
            }

            this._commands.Add(_commandShemas.OutV(param_));

            this._commandBuilder.BindBuilders(this._commands, this._formatGenerator.FromatFromTokenArray(this._commands, _tokenMiniFactory.EmptyString()));
            this._commandBuilder.Build();
            return this;
        }
        public CommandsChain OutE(ITypeToken param_)
        {
            this._commands = new List<ICommandBuilder>();

            if (this._commandBuilder.Tokens != null)
            {
                this._commands.Add(this._commandBuilder);
            }

            this._commands.Add(_commandShemas.OutE(param_));

            this._commandBuilder.BindBuilders(this._commands, this._formatGenerator.FromatFromTokenArray(this._commands, _tokenMiniFactory.EmptyString()));
            this._commandBuilder.Build();
            return this;
        }
        public CommandsChain In(ITypeToken param_)
        {
          this._commands = new List<ICommandBuilder>();

          if (this._commandBuilder.Tokens != null)
          {
              this._commands.Add(this._commandBuilder);
          }

          this._commands.Add(_commandShemas.In(param_));

          this._commandBuilder.BindBuilders(this._commands, this._formatGenerator.FromatFromTokenArray(this._commands, _tokenMiniFactory.EmptyString()));
          this._commandBuilder.Build();
          return this;
        }
        public CommandsChain Out(ITypeToken param_)
        {
          this._commands = new List<ICommandBuilder>();

          if (this._commandBuilder.Tokens != null)
          {
              this._commands.Add(this._commandBuilder);
          }

          this._commands.Add(_commandShemas.Out(param_));

          this._commandBuilder.BindBuilders(this._commands, this._formatGenerator.FromatFromTokenArray(this._commands, _tokenMiniFactory.EmptyString()));
          this._commandBuilder.Build();
          return this;
        }
        

        public CommandsChain Dot()
        {
            this._commands = new List<ICommandBuilder>();
            List<ITypeToken> tl = new List<ITypeToken>() { _tokenMiniFactory.Dot() };

            if (this._commandBuilder.Tokens != null)
            {
                this._commands.Add(this._commandBuilder);
            }

            this._commands.Add(_commandFactory.CommandBuilder(_tokenMiniFactory, _formatFactory, tl, _tokenMiniFactory.EmptyString()));

            this._commandBuilder.BindBuilders(this._commands, this._formatGenerator.FromatFromTokenArray(this._commands, _tokenMiniFactory.EmptyString()));
            this._commandBuilder.Build();
            return this;
        }
        public CommandsChain Gap()
        {
            this._commands = new List<ICommandBuilder>();
            List<ITypeToken> tl = new List<ITypeToken>() { _tokenMiniFactory.Gap() };

            if (this._commandBuilder.Tokens != null)
            {
                this._commands.Add(this._commandBuilder);
            }

            this._commands.Add(_commandFactory.CommandBuilder(_tokenMiniFactory, _formatFactory, tl, _tokenMiniFactory.EmptyString()));

            this._commandBuilder.BindBuilders(this._commands, this._formatGenerator.FromatFromTokenArray(this._commands, _tokenMiniFactory.EmptyString()));
            this._commandBuilder.Build();
            return this;
        }

        public CommandsChain Coma()
        {
            this._commands = new List<ICommandBuilder>();
            List<ITypeToken> tl = new List<ITypeToken>(){_tokenMiniFactory.Coma()};

            if (this._commandBuilder.Tokens != null)
            {
                this._commands.Add(this._commandBuilder);
            }

            this._commands.Add(_commandFactory.CommandBuilder(_tokenMiniFactory, _formatFactory, tl, _tokenMiniFactory.EmptyString()));

            this._commandBuilder.BindBuilders(this._commands, this._formatGenerator.FromatFromTokenArray(this._commands, _tokenMiniFactory.EmptyString()));
            this._commandBuilder.Build();
            return this;
        }

        public CommandsChain ClassCheck(ITypeToken param_)
        {
            this._commands = new List<ICommandBuilder>();

            if (this._commandBuilder.Tokens != null)
            {
                this._commands.Add(this._commandBuilder);
            }

            this._commands.Add(_commandShemas.ClassCheck(param_));

            this._commandBuilder.BindBuilders(this._commands, this._formatGenerator.FromatFromTokenArray(this._commands, _tokenMiniFactory.EmptyString()));
            this._commandBuilder.Build();
            return this;
        }

        public CommandsChain Expand(ITypeToken aliace_)
        {
            this._commands = new List<ICommandBuilder>();
            this._commands.Add(this._commandShemas.Expand(this._commandBuilder, aliace_));                

            this._commandBuilder.BindBuilders(this._commands, this._formatGenerator.FromatFromTokenArray(this._commands, _tokenMiniFactory.EmptyString()));
            this._commandBuilder.Build();
            return this;
        }
        public CommandsChain As(ITypeToken aliace_)
        {
            this._commands = new List<ICommandBuilder>();
            List<ITypeToken> tl = new List<ITypeToken>();

            tl.Add(_tokenMiniFactory.Gap());
            tl.Add(_tokenOrientFactory.As());        
            tl.Add(aliace_);

            if (this._commandBuilder.Tokens != null)
            {
                this._commands.Add(this._commandBuilder);
            }

            this._commands.Add(_commandFactory.CommandBuilder(_tokenMiniFactory, _formatFactory, tl, _tokenMiniFactory.EmptyString()));

            this._commandBuilder.BindBuilders(this._commands, this._formatGenerator.FromatFromTokenArray(this._commands, _tokenMiniFactory.EmptyString()));
            this._commandBuilder.Build();
            return this;
        }

        public CommandsChain Update(ITypeToken param_ = null)
        {
            this._commands = new List<ICommandBuilder>();

            if (this._commandBuilder.Tokens != null)
            {
                this._commands.Add(this._commandBuilder);
            }

            this._commands.Add(_commandShemas.Update(param_));

            this._commandBuilder.BindBuilders(this._commands
                , this._formatGenerator.FromatFromTokenArray(this._commands, _tokenMiniFactory.EmptyString()));
            this._commandBuilder.Build();

            return this;
        }

    }

    //<<<
    /// <summary>
    /// <<< deprecation possible. mmostly replaced by generator with empty string placeholder. 
    /// Complex edge filter formats replaced by groups of nested concatenation
    /// </summary>
    #region TokenFormats
    //Auth Orient URL
    public class OrientAuthenticationURLFormat : ITypeToken
    {
        public string Text {get; set;}=@"{0}:{1}/{2}/{3}";
   }
    //Command URL part format
    public class OrientCommandURLFormat : ITypeToken
    {
        public string Text {get; set;}=@"{0}:{1}/{2}/{3}/{4}"; 
   }
    public class OrientDatabaseUrlFormat : ITypeToken
    {
        public string Text {get; set;}=@"{0}/{1}";
   }


    /// </summary>
    /// command queries contains prevoius command as first parameter, 
    /// cause WHERE not intended to be used without select
    /// </summary>

    //command query part format
    public class OrientSelectClauseFormat : ITypeToken
    {
        public string Text {get; set;}=@"{0} {1} {2}";
   } 
    //Command for concatenating select command and where clause
    public class OrientWhereClauseFormat : ITypeToken
    {
        public string Text {get; set;}=@"{0} {1}";
   } 
    //create vertex command Format
    public class OrientCreateVertexCluaseFormat : ITypeToken
    {
        public string Text {get; set;}=@"{0} {1} {2} {3} {4}"; 
   }
    //delete vertex command Format
    public class OrientDeleteVertexCluaseFormat : ITypeToken
    {
        public string Text {get; set;}=@"{0} {1} {2} {3}";
   }
    //delete command Format
    public class OrientDeleteCluaseFormat : ITypeToken
    {
        public string Text {get; set;}=@"{0} {1} {2}"; 
   }
    //format for inEoutV select
    public class OrientOutEinVFormat : ITypeToken
    {
        public string Text {  get; set;}=@"{0} {1}{2} {3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18} {19} {20} {21} {22}"; 
   }
    public class OrientOutVinVFormat : ITypeToken
    {
        public string Text {get; set;}=@"{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12} {13} {14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}"; 
   }
    #endregion
    //<<<
    #region CommandBuilders

    /// <summary>
    /// Builders.
    /// Build command acording to type of passed object (class,vertes, or edge with objects referenced or ids)
    /// Not use predefined formatters 
    /// for special commands like create class/edge/vertex
    /// which not requer special format like {0}:{1}\{2} but samle , generated fro mtoken list like {0} {1} {2}
    /// but generated in lagre ammounts with differen types.
    /// </summary>
    public class OrientTokenBuilder : ITokenBuilder
    {
        //for select command
        public List<ITypeToken> Command(ITypeToken command_, ITypeToken orientObject)
        {
            List<ITypeToken> result=new List<ITypeToken>();
            result.Add(command_);
            result.Add(new OrientFromToken());
            result.Add(orientObject);
            return result;
       }

        //for delete, select conditional command
        public List<ITypeToken> Command(ITypeToken command_, ITypeToken orientObject, ITypeToken orientType)
        {
            List<ITypeToken> result=new List<ITypeToken>();
            if (command_ is OrientDeleteToken)
            {
                result.Add(command_);
                result.Add(orientType);
                //result.Add(new OrientFromToken());
                result.Add(orientObject);
           }
            if (command_ is OrientSelectToken)
            {
                result.Add(command_);
                result.Add(new OrientFromToken());
                result.Add(orientObject);
                result.Add(new OrientWhereToken());
                result.Add(orientType);
           }
            return result;
       }

        //for create command, authenticate, command
        public List<ITypeToken> Command(ITypeToken command_, ITypeToken orientObject, ITypeToken orientType, ITypeToken context_)
        {
            List<ITypeToken> result=new List<ITypeToken>();
            result.Add(command_);
            result.Add(orientType);
            result.Add(orientObject);
            result.Add(new OrientContentToken());
            result.Add(context_);
            return result;
       }

        //for create Edge from to 
        public List<ITypeToken> Command(ITypeToken command_, ITypeToken orientObject, ITypeToken orientType, ITypeToken tokenA, ITypeToken tokenB, ITypeToken context_)
        {
            tokenA.Text=tokenA.Text.Replace(@"#", "");
            tokenB.Text=tokenB.Text.Replace(@"#", "");

            List<ITypeToken> result=new List<ITypeToken>();
            result.Add(command_);
            result.Add(orientType);
            result.Add(orientObject);
            result.Add(new OrientFromToken());
            result.Add(tokenA);
            result.Add(new OrientToToken());
            result.Add(tokenB);

            if (command_ is OrientUUIDToken)
            {
                if (context_ != null && context_.Text != null && context_.Text != string.Empty)
                {
                    result.Add(new OrientContentToken());
                    result.Add(context_);
               }
           }
            if (command_ is OrientDeleteToken)
            {
                if (context_ != null && context_.Text != null && context_.Text != string.Empty)
                {
                    result.Add(context_);
               }
           }
            return result;
       }

   }

    public class OreintNewsTokenBuilder
    {
        TypeConverter typeConverter_=new TypeConverter();

        public List<ITypeToken> outEinVExp(ITypeToken command_, ITypeToken vertex_, ITypeToken edge_, ITypeToken condition_)
        {
            List<ITypeToken> result=new List<ITypeToken>();
            result.Add(command_);
            //expand
            result.Add(new OrientExpandToken());
            result.Add(new OrientRoundBraketLeftToken());

            //outE
            result.Add(new OrientOutToken());
            result.Add(new OrientEToken());
            result.Add(new OrientRoundBraketLeftToken());
            result.Add(new OrientApostropheToken());
            result.Add(edge_);
            result.Add(new OrientApostropheToken());
            result.Add(new OrientRoundBraketRightToken());
            result.Add(new OrientDotToken());
            //inv
            result.Add(new OrientInToken());
            result.Add(new OrientVToken());
            result.Add(new OrientRoundBraketLeftToken());
            result.Add(new OrientApostropheToken());
            result.Add(vertex_);
            result.Add(new OrientApostropheToken());
            result.Add(new OrientRoundBraketRightToken());

            result.Add(new OrientRoundBraketRightToken());

            result.Add(new OrientFromToken());
            result.Add(vertex_);

            if (condition_ != null && condition_.Text != null && condition_.Text != string.Empty)
            {
                result.Add(new OrientWhereToken());
                result.Add(condition_);
           }

            result.Add(new OrientRoundBraketLeftToken());
            result.Add(new OrientApostropheToken());
            result.Add(new OrientRoundBraketRightToken());
            return result;
       }

        public List<ITypeToken> outVinVcnd(Type object_, ITypeToken property_, ITypeToken conda_, ITypeToken condb_)
        {
            List<ITypeToken> result=new List<ITypeToken>();

            if (object_.BaseType==typeof(V) || object_.BaseType==typeof(E))
            {

                //outV
                result.Add(new OrientOutToken());
                result.Add(new OrientVToken());
                result.Add(new OrientRoundBraketLeftToken());
                result.Add(new OrientApostropheToken());
                result.Add(typeConverter_.Get(object_));
                result.Add(new OrientApostropheToken());
                result.Add(new OrientRoundBraketRightToken());
                result.Add(new OrientDotToken());
                result.Add(property_);

                result.Add(new OrientEqualsToken());

                result.Add(new OrientApostropheToken());
                result.Add(conda_);
                result.Add(new OrientApostropheToken());

                result.Add(new OrientAndToken());

                //inv
                result.Add(new OrientInToken());
                result.Add(new OrientVToken());
                result.Add(new OrientRoundBraketLeftToken());
                result.Add(new OrientApostropheToken());
                result.Add(typeConverter_.Get(object_));
                result.Add(new OrientApostropheToken());
                result.Add(new OrientRoundBraketRightToken());
                result.Add(new OrientDotToken());
                result.Add(property_);

                result.Add(new OrientEqualsToken());

                result.Add(new OrientApostropheToken());
                result.Add(condb_);
                result.Add(new OrientApostropheToken());

           }
            return result;
       }

   }
    /// <summary>creates collection of tokens
    /// builds add,delete,create commands from token amount
    /// </summary>
    public class OrientCommandBuilderImplicit : ITokenBuilderTypeGen
    {

        ITypeTokenConverter _typeConverter;

        public void BindConverter(ITypeTokenConverter typecOnverter_)
        {
            this._typeConverter=typecOnverter_;
       }

        public List<ITypeToken> Command(ITypeToken name_, ITypeToken type_)
        {
            List<ITypeToken> result=new List<ITypeToken>();
            result.Add(name_);
            result.Add(type_);
            return result;
       }


        public List<ITypeToken> Command(ITypeToken command_, IOrientObject orientClass_)
        {
            List<ITypeToken> result=new List<ITypeToken>();
            result.Add(command_);
            result.Add(_typeConverter.GetBase(orientClass_.GetType()));
            result.Add(_typeConverter.Get(orientClass_.GetType()));


            return result;
       }


        public List<ITypeToken> Command(ITypeToken command_, IOrientObject orientClass_, ITypeToken content=null)
        {
            List<ITypeToken> result=new List<ITypeToken>();
            result.Add(command_);
            result.Add(_typeConverter.GetBase(orientClass_.GetType()));
            result.Add(_typeConverter.Get(orientClass_.GetType()));
            if (content != null)

            {
                if (orientClass_ is IOrientClass)
                {
                    result.Add(new OrientExtendsToken());
               }
                if (orientClass_ is IOrientVertex)
                {
                    result.Add(new OrientContentToken());
               }



                result.Add(content);
           }
            return result;
       }
        public List<ITypeToken> Command(ITypeToken command_, Type orientClass_, ITypeToken content=null)
        {
            List<ITypeToken> result=new List<ITypeToken>();
            result.Add(command_);
            result.Add(_typeConverter.GetBase(orientClass_.GetType()));
            result.Add(_typeConverter.Get(orientClass_.GetType()));
            if (content != null)

            {
                if (_typeConverter.GetBase(orientClass_.GetType()) is IOrientClass)
                {
                    result.Add(new OrientExtendsToken());
               }
                if (_typeConverter.GetBase(orientClass_.GetType()) is IOrientVertex)
                {
                    result.Add(new OrientContentToken());
               }




                result.Add(content);
           }
            return result;
       }


        public List<ITypeToken> Command(ITypeToken command_, IOrientObject orientClass_, IOrientObject orientProperty_, ITypeToken orientType_, bool mandatory=false, bool notnull=false)
        {
            List<ITypeToken> result=new List<ITypeToken>();
            result.Add(command_);
            result.Add(_typeConverter.GetBase(orientProperty_.GetType()));
            result.Add(_typeConverter.Get(orientClass_.GetType()));
            result.Add(new OrientDotToken());
            result.Add(_typeConverter.Get(orientProperty_.GetType()));
            result.Add(orientType_);
            result.Add(new OrientRoundBraketLeftToken());
            result.Add(new OrientMandatoryToken());
            if (mandatory)
            {
                result.Add(new OrientTRUEToken());
           }
            else {result.Add(new OrientFLASEToken());}
            result.Add(new OrientNotNULLToken());

            result.Add(new OrientCommaToken());

            if (notnull)
            {
                result.Add(new OrientTRUEToken());
           }
            else {result.Add(new OrientFLASEToken());}
            result.Add(new OrientRoundBraketRightToken());





            return result;
       }


        public List<ITypeToken> Command(ITypeToken command_, IOrientObject orientClass_, ITypeToken from, ITypeToken to, ITypeToken content=null)
        {
            List<ITypeToken> result=new List<ITypeToken>();
            result.Add(command_);
            result.Add(_typeConverter.GetBase(orientClass_.GetType()));
            result.Add(_typeConverter.Get(orientClass_.GetType()));


            result.Add(new OrientFromToken());
            result.Add(from);
            result.Add(new OrientToToken());
            result.Add(to);
            if (content != null)
            {
                result.Add(new OrientContentToken());
                result.Add(content);
           }

            return result;
       }

   }

    /// <summary>
    /// Builder with exlicitly named commands
    /// </summary>
    public class OrientTokenBuilderExplicit
    {

        //Create class cluase (type check) with extends class option
        public List<ITypeToken> Create(OrientClassToken classType_, ITypeToken extendsClassType_=null)
        {

            List<ITypeToken> result=new List<ITypeToken>() {
                new OrientUUIDToken()
           };

            if (classType_ is OrientClassToken)
            {
                result.Add(new OrientClassToken());
                result.Add(classType_);

                if (extendsClassType_ != null)
                {
                    result.Add(new OrientExtendsToken());
                    result.Add(extendsClassType_);
               }
           }
            return result;
       }
        //Create  vertex cluase (type check) with content optiona
        public List<ITypeToken> Create(OrientVertexToken classType_, ITypeToken extendsClassType_=null)
        {
            List<ITypeToken> result=new List<ITypeToken>() {
                new OrientUUIDToken()
           };
            if (classType_ is OrientVertexToken)
            {
                result.Add(new OrientVertexToken());
                result.Add(classType_);

                if (extendsClassType_ != null)
                {
                    result.Add(new OrientContentToken());
                    result.Add(extendsClassType_);
               }
           }
            return result;
       }
        //Create property cluase
        public List<ITypeToken> Create(ITypeToken className_, ITypeToken propertyName_, ITypeToken propertyType_,
        bool mandatory_=false, bool notnull=false)
        {
            List<ITypeToken> result=new List<ITypeToken>() {
                new OrientUUIDToken(), new OrientPropertyToken(),className_, new OrientDotToken(),propertyName_,propertyType_
           };

            result.Add(new OrientRoundBraketLeftToken());
            result.Add(new OrientMandatoryToken());

            if (mandatory_)
            {
                result.Add(new OrientTRUEToken());
           }
            else {result.Add(new OrientFLASEToken());}

            result.Add(new OrientCommaToken());
            result.Add(new OrientNotNULLToken());

            if (notnull)
            {
                result.Add(new OrientTRUEToken());
           }
            else {result.Add(new OrientFLASEToken());}

            result.Add(new OrientRoundBraketRightToken());

            return result;
       }
        //Create edge clause
        public List<ITypeToken> Create(ITypeToken className_, ITypeToken from_, ITypeToken to_)
        {

            List<ITypeToken> result=new List<ITypeToken>(){
                new OrientUUIDToken(), new OrientEdgeToken(), className_, new OrientFromToken(),
                from_,
                new OrientToToken(),
                to_
           };

            return result;
       }
        //For select from cluases (Vertex,edge)
        public List<ITypeToken> Select(ITypeToken orientType_, ITypeToken class_)
        {
            List<ITypeToken> result=new List<ITypeToken>() {
                new OrientSelectToken(),orientType_, new OrientFromToken(), class_
           };

            return result;
       }
        //Where clauses receiveing content< which must be strictly parametrized in UOW
        public List<ITypeToken> Where(ITypeToken orientType_, ITypeToken content_)
        {
            List<ITypeToken> result=new List<ITypeToken>() {
                new OrientWhereToken(),content_
           };

            return result;
       }
        //delete vertex,edge,or class
        public List<ITypeToken> Delete(ITypeToken classType_, ITypeToken classname_)
        {
            List<ITypeToken> result=new List<ITypeToken>();
            if (classType_ is OrientVertexToken)
            {
                result.Add(new OrientDeleteToken());
                result.Add(classType_);
           }
            if (classType_ is OrientEdgeToken)
            {
                result.Add(new OrientDeleteToken());
                result.Add(classType_);
           }
            if (classType_ is OrientClassToken)
            {
                result.Add(new OrientDropToken());
                result.Add(classType_);
           }
            if (result.Count() != 0)
            {
                result.Add(classname_);
           }
            return result;
       }

        public List<ITypeToken> Function(ITypeToken function_, ICommandBuilder params_)
        {
            List<ITypeToken> result=new List<ITypeToken>();
            result.Add(function_);
            result.AddRange(params_.Tokens);
            return result;
       }
   }

    #endregion
    //<<<
    #region TokenListsBuilders

    ///<summary>buider for commands with format
    ///mostly used for URLS (auth)</summary
    public class OrientCommandBuilder : CommandBuilder
    {
      
        public OrientCommandBuilder(ITokenMiniFactory tokenFactory_,  IFormatFactory formatFactory_) 
            : base(tokenFactory_, formatFactory_)
        {

       }
       
   }

    //<<<deprecation possible, replaced with type convertible commandbuilder
    ///<summary>   
    ///Query builders
    ///class segregation for different cluse builders
    ///</summary>
    //Authentication URL build
    public class OrientAuthenticationURIBuilder : CommandBuilder
    {
        public OrientAuthenticationURIBuilder(ITokenMiniFactory tokenFactory_, IFormatFactory formatfactory_)
             : base(tokenFactory_, formatfactory_)
        {

       }
   }
    //Command URL build
    public class OrientCommandURIBuilder : CommandBuilder
    {
        public OrientCommandURIBuilder(ITokenMiniFactory tokenFactory_, IFormatFactory formatfactory_)
             : base(tokenFactory_, formatfactory_)
        {

       }
        public OrientCommandURIBuilder(List<ICommandBuilder> texts_, ITypeToken FormatPattern_)
          : base(texts_,FormatPattern_)
        {

       }
   }

    public class OrientSelectClauseBuilder : CommandBuilder
    {
        public OrientSelectClauseBuilder(ITokenMiniFactory tokenFactory_, IFormatFactory formatfactory_)
             : base(tokenFactory_, formatfactory_)
        {

       }
   }
    public class OrientWhereClauseBuilder : CommandBuilder
    {
        public OrientWhereClauseBuilder(ITokenMiniFactory tokenFactory_, IFormatFactory formatfactory_)
             : base(tokenFactory_, formatfactory_)
        {

       }
   }

    public class OrientCreateClauseBuilder : CommandBuilder
    {
        public OrientCreateClauseBuilder(ITokenMiniFactory tokenFactory_, IFormatFactory formatfactory_)
             : base(tokenFactory_, formatfactory_) {

       }
   }
    public class OrientDeleteClauseBuilder : CommandBuilder
    {
        public OrientDeleteClauseBuilder(ITokenMiniFactory tokenFactory_, IFormatFactory formatfactory_)
             : base(tokenFactory_, formatfactory_)
        {

       }
   }

    public class OrientNestedSelectClauseBuilder : CommandBuilder
    {
        public OrientNestedSelectClauseBuilder(ITokenMiniFactory tokenFactory_, IFormatFactory formatfactory_)
             : base(tokenFactory_, formatfactory_)
        {

       }
   }
    #endregion
    //<<<
    ///<summary>predefined url token collections
    ///prefered change to predefined url and command builds</summary>

    public static class TokenRepo
    {
        public static List<ITypeToken> authUrl=new List<ITypeToken>() {new OrientHost(), new OrientPort(), new OrientAuthenticateToken(), new OrientDatabaseNameToken()};
        public static List<ITypeToken> commandUrl=new List<ITypeToken>() {new OrientHost(), new OrientPort(), new OrientCommandToken(), new OrientDatabaseNameToken(), new OrientCommandSQLTypeToken()};
        public static List<ITypeToken> addDbURL=new List<ITypeToken>() {new OrientHost(), new OrientPort(), new OrientURLDatabaseToken()};
    }
	
    ///<summary>Converts from model poco classes types to ItypeToken types
    ///</summary>
    public class TypeConverter : ITypeTokenConverter
    {

        internal Dictionary<Type, ITypeToken> types;

        public TypeConverter()
        {

            types=new Dictionary<Type, ITypeToken>();

            types.Add(typeof(V), new OrientVToken());
            types.Add(typeof(E), new OrientEToken());

            types.Add(typeof(Person), new OrientPersonToken());
            types.Add(typeof(Unit), new OrientUnitToken());

            types.Add(typeof(MainAssignment), new OrientMainAssignmentToken());
            types.Add(typeof(SubUnit), new OrientSubUnitToken());

            types.Add(typeof(UserSettings), new OrientUserSettingsToken());
            types.Add(typeof(CommonSettings), new OrientCommonSettingsToken());
            
            types.Add(typeof(TrackBirthdays), new OrientTrackBirthdaysToken());

            types.Add(typeof(Note), new OrientNoteToken());
            types.Add(typeof(Authorship), new OrientAuthorshipToken());
            types.Add(typeof(Comment), new OrientCommentToken());
            types.Add(typeof(Like), new OrientLikeToken());
            types.Add(typeof(Tag), new OrientTagToken());
            types.Add(typeof(Tagged), new OrientTaggedToken());
            types.Add(typeof(Object_SC), new OrientObjectScToken());
        }
        public void Add(Type type_, ITypeToken token_)
        {
            this.types.Add(type_, token_);
        }
        public ITypeToken Get(Type type_)
        {
            ITypeToken token_=null;
            types.TryGetValue(type_, out token_);
            if(token_==null)
            {
              Type tp=GegtypeFromAsm(type_.Name,null);
              if(tp!=null)
              {
                token_=new TextToken() {Text=tp.Name};
              }
            }
            return token_;
        }
        public ITypeToken GetBase(Type type_)
        {
            ITypeToken token_=null;
            Type tp=type_.BaseType;
            types.TryGetValue(tp, out token_);

            return token_;
        }
        public ITypeToken Get(IOrientObject object_)
        {
            ITypeToken token_=null;

            types.TryGetValue(object_.GetType(), out token_);

            return token_;
        }
        public ITypeToken GetBase(IOrientObject object_)
        {
            ITypeToken token_=null;
            Type tp=object_.GetType().BaseType;
            IOrientVertex t2=(IOrientVertex)object_;

            types.TryGetValue(object_.GetType().BaseType, out token_);

            return token_;
        }
        public ITypeToken Get(string object_)
        {
            ITypeToken token_ = null;

            token_ = Get(GegtypeFromAsm(object_));

            return token_;
        }
        public ITypeToken GetBase(string object_)
        {
            ITypeToken token_ = null;
            Type tp = object_.GetType().BaseType;
            IOrientVertex t2 = (IOrientVertex)tp;

            types.TryGetValue(object_.GetType().BaseType, out token_);

            return token_;
        }

        public Type GegtypeFromAsm(string typeName_, string asm_ = null)
        {
            Type result = null;

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            Type[] assemblyTypes = assembly.GetTypes();
            if (asm_ == null)
            {
                result = assemblyTypes.FirstOrDefault(s => s.Name == typeName_);
            }
            else
            {
                result = assemblyTypes.FirstOrDefault(s => s.FullName.Contains(asm_) && s.Name == typeName_);
            }

            return result;
        }

    }

    /// <summary>
    /// Converts CLR types to Orient types
    /// </summary>
    public class OrientCLRconverter : IPropertyConverter
    {
      internal Dictionary<Type, ITypeToken> types;

      public OrientCLRconverter()
      {
          types = new Dictionary<Type, ITypeToken>();

          types.Add(typeof(String), new OrientSTRINGToken());
          types.Add(typeof(bool), new OrientBOOLEANToken());
          types.Add(typeof(DateTime), new OrientDATETIMEToken());
          types.Add(typeof(Int64), new OrientIntToken());
          types.Add(typeof(Int32), new OrientIntToken());
      }
      public ITypeToken Get(Type type_)
      {
          ITypeToken token_ = null;

          types.TryGetValue(type_, out token_);

          return token_;
      }
      public ITypeToken GetBoolean(bool bool_)
        {
            if (bool_) { return new OrientTRUEToken(); }
            return new OrientFLASEToken();
        }

    }

    
    /// <summary>
    /// Manager for generating,sending, parsing commands to Orient db.
    /// </summary>
    public class OrientRepo:IOrientRepo
    {
        ICommandBuilder commandBody = null;
        ITypeToken result_;
        string response_;

        string dbName, requestBody, urlStr;        

        ITypeTokenConverter _typeConverter;
        IPropertyConverter _propertyConverter;

        IJsonManagers.IJsonManger _jsonmanager;
        ITokenMiniFactory _miniFactory;
        IFormatFactory _formatFactory;

        UrlShemasExplicit _urlShema;
        BodyShemas _bodyShema;
        CommandShemasExplicit _commandShema;

        IWebManagers.IWebRequestManager _webmanager;
        IWebManagers.IResponseReader _webResponseReader;

        ICommandFactory _commandFactory;
        IOrientQueryFactory _orientfactory;

        public OrientRepo
        (ITypeTokenConverter typeConverter_, IJsonManagers.IJsonManger jsonmanager_,TokenMiniFactory miniFactory_,
        UrlShemasExplicit urlShema_,
        BodyShemas bodyShema_,
        CommandShemasExplicit commandShema_,
        IWebManagers.IWebRequestManager webmanager_,
        IWebManagers.IResponseReader webResponseReader_,
        ICommandFactory commandFactory_,
        IFormatFactory formatFactory_,
        IOrientQueryFactory orientfactory_,
        IPropertyConverter propertyConverter_)
        {        
          this._typeConverter=typeConverter_;
          this._jsonmanager=jsonmanager_;
          this._miniFactory=miniFactory_;
          this._urlShema=urlShema_;
          this._bodyShema=bodyShema_;
          this._commandShema=commandShema_;
          this._webmanager=webmanager_;
          this._webResponseReader=webResponseReader_;
          this._commandFactory=commandFactory_;
          this._formatFactory=formatFactory_;
          this._orientfactory=orientfactory_;
          this._propertyConverter=propertyConverter_;
        }


        void BindCommandBody()
        {
          if (this.commandBody!=null)
          {
            this.requestBody = _bodyShema.Command(this.commandBody).Build().GetText();
          }
        }
        void BindBatchBody()
        {
          if (this.commandBody!=null)
          {
            this.requestBody=_bodyShema.Batch(this.commandBody).Build().GetText();
          }
        }
        void BindCommandUrl()
        {            
            this.urlStr = _urlShema.Command(_miniFactory.NewToken(this.dbName)).Build().GetText();
        }
        void BindBatchUrl()
        {
            this.urlStr = _urlShema.Batch(_miniFactory.NewToken(this.dbName)).Build().GetText();
        }

        void BindWebRequest()
        {
            _webmanager.SwapRequestsURL(this.urlStr);
            if (this.commandBody != null)
            {
                _webmanager.SetContent(requestBody);
            }
        }
        void ReadResponseStr(string method_,ITypeToken passed_=null)
        {
            response_= _webResponseReader.ReadResponse(_webmanager.GetResponse64(method_));

            if (this.response_ != null && this.response_ != string.Empty)
            {
                this.result_ = passed_;
            }
        }

        public PropertyInfo[] Props<T>() 
        where T:IOrientObject
        {
            PropertyInfo[] psc = typeof(T).GetType().GetProperties();
           
            foreach (PropertyInfo ps in psc)
            {
              Type pt = null;

              try
              {
                pt = typeof(T).GetType().GetProperty(ps.Name).GetType();
              }
              catch (Exception e) { }

              if (pt != null)
              {
              }
            }

            return psc;
        }

        public CommandsChain NewChain()
        {
            return new CommandsChain(_miniFactory, _orientfactory, _formatFactory, _commandFactory);
        }

        void CheckDbName(string input_)
        {
          string name_ = ConfigurationManager.AppSettings["OrientDevDB"];
          if(string.IsNullOrEmpty(input_)){
            if(string.IsNullOrEmpty(this.dbName)){
              if(string.IsNullOrEmpty(name_)){
                throw new Exception("No db name passed");
              }
              BindDbName(name_);
            }
            BindDbName(this.dbName);
          }else{
            BindDbName(input_);
          }
        }
        void CheckUrl(string input_)
        {
          string name_ = ConfigurationManager.AppSettings["OrientDevHost"];

          if(string.IsNullOrEmpty(input_)){
            if(string.IsNullOrEmpty(this.urlStr)){
              if(string.IsNullOrEmpty(name_)){
                throw new Exception("No db name passed");
              }
              BindUrlName(name_);
            }
            BindUrlName(this.urlStr);
          }else{
            BindUrlName(input_);
          }
        }
        public void BindDbName(string dbName_)
        {
          this.dbName=dbName_;
        }
        public void BindUrlName(string input_)
        {
          this.urlStr=input_;
        }
        public string GetResult()
        {
            return this.result_.Text;
        }

        public IOrientRepo CreateDb(string dbName_=null,string host=null) {
        
          CheckDbName(dbName_);
          CheckUrl(host);

          _webmanager.AddRequest(_urlShema.Database(_miniFactory.NewToken(this.dbName)).GetText());
          try
          {             
            ReadResponseStr("POST");
            if (this.response_ != null)
            {         
              this.result_ = _miniFactory.Created();
            }
          }
          catch (Exception e) { }
          
          return this;
        }
        public IOrientRepo DeleteDb(string dbName_=null,string host=null) {
          CheckDbName(dbName_);
          CheckUrl(host);
            _webmanager.AddRequest(_urlShema.Database(_miniFactory.NewToken(this.dbName)).GetText());
            try
            {
                ReadResponseStr("DELETE");

                if (this.response_ != null)
                {                   
                    this.result_ = _miniFactory.Deleted();
                }
            }
            catch (Exception e) { }

            return this;
        }
        public IOrientRepo Delete<T>(T item=null,string condition_=null,string dbName_=null) 
            where T:class, IorientDefaultObject
        {
          CheckDbName(dbName_);

          ITypeToken token_=_typeConverter.Get(typeof(T));
          List<ITypeToken> tt = null;
          ICommandBuilder where_ = null;
          Type baseType=typeof(T).BaseType;

          if (condition_ != null)
          {
              tt=new List<ITypeToken>() { _miniFactory.NewToken(condition_) };
          }
          else
          {
              if (item != null)
              {
                  tt=new List<ITypeToken>() { _miniFactory.NewToken("@rid=" + item.id) };
              }
          }
 
          if (tt != null)
          {
              where_ =
                  _commandFactory.CommandBuilder(_miniFactory, _formatFactory, tt, _miniFactory.EmptyString()).Build();
          }
      

          if (baseType == typeof(V))
          {
              this.commandBody = NewChain().Delete().Vertex(token_).Where(where_)
                  .GetBuilder().Build();
          }
          if (baseType == typeof(E))
          {
              this.commandBody = NewChain().Delete().Edge(token_).Where(where_)
                  .GetBuilder().Build();
          }

          BindBatchBody();
          BindBatchUrl();
          BindWebRequest();
          ReadResponseStr("POST", _miniFactory.Deleted());
                         
          return this;
        }     
        public IOrientRepo DeleteEdge<T>(string from,string to,string condition_=null,string dbName_=null) 
          where T :class, IOrientEdge
        {
           
            ITypeToken relTypeToken_ = _typeConverter.Get(typeof(T));
            List<ITypeToken> tt = null;
            ICommandBuilder where_ = null;
            Type baseType = typeof(T).BaseType;
            ITypeToken fromID = _miniFactory.NewToken(from);
            ITypeToken toID = _miniFactory.NewToken(to);
            CheckDbName(dbName_);

            if (condition_ != null)
            {
                tt = new List<ITypeToken>(){ _miniFactory.NewToken(condition_)};
            }
            if (tt!=null)
            {
                where_ =
                   _commandFactory.CommandBuilder(_miniFactory, _formatFactory, tt, _miniFactory.EmptyString()).Build();
            }
            if (baseType==typeof(E))
            {
                commandBody = NewChain().Delete().Edge(relTypeToken_).FromV(fromID).ToV(toID).Where(where_)
                    .GetBuilder().Build();
            }

            BindCommandUrl();
            BindCommandBody();
            BindWebRequest();
            ReadResponseStr("POST", _miniFactory.Deleted());

            return this;
        }
        /// <summary>
        /// delete Edge from to vertex
        /// </summary>
        /// <typeparam name="T">Relation type</typeparam>
        /// <typeparam name="K">Node from type</typeparam>
        /// <typeparam name="C">Node to type</typeparam>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="condition_">Condition to filter concrete Relations. If null all relations deleted.</param>
        /// <param name="dbName_">Database name</param>
        /// <returns></returns>
        public IOrientRepo DeleteEdge<T,K,C>(K from,C to,string condition_=null,string dbName_=null)
            where T:IOrientEdge where K:IOrientVertex where C:IOrientVertex
        {
            
            CheckDbName(dbName_);

            ITypeToken relTypeToken_ = _typeConverter.Get(typeof(T));
            List<ITypeToken> tt = null;
            ICommandBuilder where_ = null;
            Type baseType = typeof(T).BaseType;
            ITypeToken fromID = null;
            ITypeToken toID = null;

            if (from != null)
            {
                fromID = _miniFactory.NewToken(from.id);
            }
            if (to != null)
            {
                toID = _miniFactory.NewToken(to.id);
            }           

            if (condition_ != null)
            {
                tt = new List<ITypeToken>() { _miniFactory.NewToken(condition_) };
            }
            if (tt != null)
            {
                where_ =
                   _commandFactory.CommandBuilder(_miniFactory, _formatFactory, tt, _miniFactory.EmptyString()).Build();
            }

            if (baseType == typeof(E))
            {
                if (from != null && to != null)
                {
                    this.commandBody=NewChain().Delete().Edge(relTypeToken_).FromV(fromID).ToV(toID).Where(where_)
                        .GetBuilder().Build();
                }else{
                    this.commandBody=NewChain().Delete().Edge(relTypeToken_).Where(where_)
                        .GetBuilder().Build();
                }
            }

            BindCommandUrl();
            BindCommandBody();
            BindWebRequest();
            ReadResponseStr("POST", _miniFactory.Deleted());
           
            return this;
        }

        public IOrientRepo CreateClass(string class_,string extends_,string dbName_=null)
        {
            CheckDbName(dbName_);

            ITypeToken clTk= _miniFactory.NewToken(class_);
            ITypeToken extTk = _miniFactory.NewToken(extends_);

            if (clTk!=null && extTk!=null && clTk.Text!=string.Empty && extTk.Text!=null)
            {
                this.commandBody = NewChain().Create().Class(clTk).Extends(extTk)
                    .GetBuilder().Build();

                CheckDbName(dbName_);

                BindCommandUrl();
                BindCommandBody();
                BindWebRequest();
                ReadResponseStr("POST", _miniFactory.Created());
                
            }
            return this;
        }
        public Type CreateClass<T,K>(string dbName_=null)
          where T:IOrientEntity where K:IOrientEntity 
        {
            CheckDbName(dbName_);

            ITypeToken clTk = _typeConverter.Get(typeof(T));
            ITypeToken extTk = _typeConverter.Get(typeof(K));
            Type ret_ = null;

            if (clTk != null && extTk != null && clTk.Text != string.Empty && extTk.Text != null)
            {
                this.commandBody = NewChain().Create().Class(clTk).Extends(extTk)
                    .GetBuilder().Build();               

                CheckDbName(dbName_);

                BindCommandUrl();
                BindCommandBody();
                BindWebRequest();                
                ReadResponseStr("POST", _miniFactory.Created());

                if (this.response_ != null && this.response_ != string.Empty)
                {
                    try
                    {
                        ret_ = clTk.GetType();
                    }
                    catch (Exception e) { }
                }
                
            }

            return ret_;
        }
    
        public IOrientRepo CreateProperty(string class_,string property_,Type type_,bool mandatory_,bool notnull_,string dbName_=null)
        {
          CheckDbName(dbName_);

            ITypeToken clTk = _miniFactory.NewToken(class_);
            ITypeToken propTk = _miniFactory.NewToken(property_);
            ITypeToken orientType = this._propertyConverter.Get(type_);
            ITypeToken mandatoryTk = this._propertyConverter.GetBoolean(mandatory_);
            ITypeToken notNnullTk = this._propertyConverter.GetBoolean(notnull_);            

            this.commandBody = NewChain().Create().Property(clTk, propTk,
            orientType, mandatoryTk, notNnullTk)
                .GetBuilder().Build();

            BindCommandUrl();
            BindCommandBody();
            BindWebRequest();
            ReadResponseStr("POST", _miniFactory.Created());           

            return this;
        }
        public T CreateProperty<T>(T item=null,string dbName_=null) 
          where T:class,IorientDefaultObject
        {
            CheckDbName(dbName_);
            T ret_ = null;
           IEnumerable<PropertyInfo> poperties_ = from s in typeof(T).GetProperties() select s;

            foreach (PropertyInfo ps in poperties_)
            {
                this.response_=null;
                Type pt=null;
               
                try
                {
                    pt = item.GetType().GetProperty(ps.Name).GetValue(item).GetType();
                }
                catch (Exception e) { }
                if(pt==null) {
                  pt=ps.PropertyType;
                }
                if (pt != null && (ps.Name!="In"||ps.Name!="Out"))
                {

                    ITypeToken clTk = _typeConverter.Get(typeof(T));
                    ITypeToken propTk = _miniFactory.NewToken(ps.Name);
                    ITypeToken orientType = this._propertyConverter.Get(pt);
                    ITypeToken mandatoryTk = this._propertyConverter.GetBoolean(false);
                    ITypeToken notNnullTk = this._propertyConverter.GetBoolean(false);

          ///not working - always flase for nullable properties
          //if (pt.IsGenericType &&
          //pt.GetGenericTypeDefinition() == typeof(Nullable<>))
          //{
          //    mandatoryTk = this._propertyConverter.GetBoolean(false);
          //    notNnullTk = this._propertyConverter.GetBoolean(false);
          //}
          //else
          //{
          //    mandatoryTk = this._propertyConverter.GetBoolean(true);
          //    notNnullTk = this._propertyConverter.GetBoolean(true);
          //}

            Mandatory mnd = PropertyTryReturnAttribute<Mandatory>(ps);
            if(mnd!=null){if(mnd.isMandatory){
              mandatoryTk = this._propertyConverter.GetBoolean(true);
              notNnullTk = this._propertyConverter.GetBoolean(true);
            }}

                    if (clTk != null && propTk != null && orientType != null)
                    {
                        if (pt.IsGenericType &&
                        pt.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            mandatoryTk = this._propertyConverter.GetBoolean(false);
                            notNnullTk = this._propertyConverter.GetBoolean(false);
                        }

                        this.commandBody = NewChain().Create().Property(clTk, propTk,
                        orientType, mandatoryTk, notNnullTk)                        
                            .GetBuilder().Build();
                      
                        BindCommandUrl();
                        BindCommandBody();
                        BindWebRequest();
                        ReadResponseStr("POST", _miniFactory.Created());                                                

                        if (ps.Name == "GUID")
                        {
                            AlterProperty(clTk, propTk, _orientfactory.UUIDToken());
                        }
                    }

                    if (this.response_ != null && this.response_ != string.Empty)
                    {
                        try
                        {
                            ret_ = _jsonmanager.DeserializeFromParentNode<T>(this.response_, "result").FirstOrDefault();
                        }
                        catch (Exception e) { }
                    }
                }

            }
           
            return ret_;
        }        

        public IOrientRepo CreateVertex(string vertex,string content_=null,string dbName_=null)
        {
            ITypeToken clTk = _miniFactory.NewToken(vertex);
            ITypeToken prsTk = _miniFactory.NewToken(content_.Replace("\"", "\\\""));

            CheckDbName(dbName_);

            if (clTk != null && prsTk != null)
            {
                //_commandFactory.CommandBuilder(_miniFactory, _formatFactory, prsTk, _miniFactory.NewToken("{0}"));

                this.commandBody = NewChain().Create().Vertex(clTk).Content(prsTk)
                    .GetBuilder().Build();

                BindBatchUrl();
                BindBatchBody();
                BindWebRequest();
                ReadResponseStr("POST", _miniFactory.Created());
               
            }
            return this;
        }
        public T CreateVertex<T>(string content_,string dbName_=null)
            where T:class,IOrientVertex
        {
          CheckDbName(dbName_);

          T ret_ = null;
          T object_=_jsonmanager.DeserializeFromParentNodeStringObj<T>(content_);
          ITypeToken clTk=_typeConverter.Get(typeof(T));
          ITypeToken prsTk=_miniFactory.NewToken(content_.Replace("\"", "\\\""));

          

          if (clTk != null && prsTk != null)
          {
              //_commandFactory.CommandBuilder(_miniFactory, _formatFactory, prsTk, _miniFactory.NewToken("{0}"));

              this.commandBody = NewChain().Create().Vertex(clTk).Content(prsTk)
                  .GetBuilder().Build();

              BindBatchUrl();
              BindBatchBody();
              BindWebRequest();
              ReadResponseStr("POST", _miniFactory.Created());

              if (this.response_ != null && this.response_ != string.Empty)
              {
                  try
                  {
                      ret_ = _jsonmanager.DeserializeFromParentNode<T>(this.response_, "result").FirstOrDefault();
                  }
                  catch (Exception e) { }
              }

          }
          return ret_;
        }      
        public T CreateVertex<T>(IOrientVertex vertex,string dbName_=null) 
          where T: class,IOrientVertex
        {
          CheckDbName(dbName_);

          ITypeToken clTk=_typeConverter.Get(typeof(T));
          ITypeToken prsTk = _miniFactory.NewToken(_jsonmanager.SerializeObject(vertex)
          .Replace("\"", "\\\""));

          T ret_=null;
           
          if (clTk != null && prsTk != null)
          {
              //_commandFactory.CommandBuilder(_miniFactory, _formatFactory, prsTk, _miniFactory.NewToken("{0}"));

              this.commandBody = NewChain().Create().Vertex(clTk).Content(prsTk)
                  .GetBuilder().Build();

              BindBatchUrl();
              BindBatchBody();
              BindWebRequest();
              ReadResponseStr("POST", _miniFactory.Created());

              if (this.response_!=null && this.response_!=string.Empty)
              {
                  try
                  {
                      ret_=_jsonmanager.DeserializeFromParentNode<T>(this.response_, "result").FirstOrDefault();
                  }
                  catch (Exception e) { }
              }
          }

          return ret_;
        }

        public T CreateEdge<T>(IOrientEdge edge_,IOrientVertex vFrom,IOrientVertex vTo,string dbName_=null) 
            where T:class,IOrientEdge            
        {
          CheckDbName(dbName_);

          T ret_ = null;
          //edge type string for query
          ITypeToken edgeTk=_typeConverter.Get(typeof(T));
          //edge count
          ITypeToken edgCnt=_miniFactory.NewToken(_jsonmanager.SerializeObject(edge_).Replace("\"", "\\\""));
          if(edgCnt==null)
          {
              edgCnt=null;
          }

          if(vFrom!=null&&vTo!=null){

              //from vertex id string
              ITypeToken fromId = _miniFactory.NewToken(vFrom.id);
              //to vertex id string
              ITypeToken toId = _miniFactory.NewToken(vTo.id);


              //ITypeToken fromTk = _typeConverter.Get(typeof(K));
              //ITypeToken toTk = _typeConverter.Get(typeof(C));
                       

              if (edgeTk != null && fromId != null && toId != null)
              {

                  //_commandFactory.CommandBuilder(_miniFactory, _formatFactory, prsTk, _miniFactory.NewToken("{0}"));

                  this.commandBody = NewChain().Create().Edge(edgeTk).FromV(fromId).ToV(toId).Content(edgCnt)
                      .GetBuilder().Build();

                  BindBatchUrl();
                  BindBatchBody();
                  BindWebRequest();
                  ReadResponseStr("POST", _miniFactory.Created());

                  if (this.response_ != null && this.response_ != string.Empty)
                  {
                      try
                      {
                          ret_ = _jsonmanager.DeserializeFromParentNode<T>(this.response_, "result").FirstOrDefault();
                      }
                      catch (Exception e) { }
                  }

              }
          }

          return ret_;
        }

        /// <summary>
        /// Searches with any type by Condition. Type not parsed to DB.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ID_"></param>
        /// <param name="cond_"></param>
        /// <param name="dbName_"></param>
        /// <returns></returns>
        public IEnumerable<T> SelectByIDWithCondition<T> (string ID_,string cond_=null,string dbName_=null)
        where T:class,IorientDefaultObject
        {
          IEnumerable<T> result=null;
          CheckDbName(dbName_);
          List<ITypeToken> tt=null;
          ICommandBuilder where_=null;
          ITypeToken id_=_miniFactory.NewToken(ID_);

            if (cond_ != null)
            {
                tt=new List<ITypeToken>(){_miniFactory.NewToken(cond_)};

                where_=
                  _commandFactory.CommandBuilder(_miniFactory, _formatFactory, tt, _miniFactory.EmptyString()).Build();
            }

            if (id_.Text!= null)
            {
                //_commandFactory.CommandBuilder(_miniFactory, _formatFactory, prsTk, _miniFactory.NewToken("{0}"));

                this.commandBody =
                  NewChain().From(id_).Select().Where(where_).GetBuilder().Build();

                BindBatchUrl();
                BindBatchBody();
                BindWebRequest();
                ReadResponseStr("POST", _miniFactory.Created());

                if (this.response_ != null && this.response_ != string.Empty)
                {
                    try
                    {
                        result=_jsonmanager.DeserializeFromParentNode<T>(this.response_, "result");
                    }
                    catch (Exception e) { }
                }
            }
          return result;
        }
        /// <summary>
        /// Searchersany type by type name parsed to DB.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t_"></param>
        /// <param name="dbName_"></param>
        /// <returns></returns>
        public IEnumerable<T> SelectAll<T>(IorientDefaultObject t_,string dbName_) where T:class,IorientDefaultObject
        {
            CheckDbName(dbName_);
            ITypeToken pTk = _typeConverter.Get(t_);
            IEnumerable<T> ret_ = new List<T>();

            if (t_ != null)
            {
                //_commandFactory.CommandBuilder(_miniFactory, _formatFactory, prsTk, _miniFactory.NewToken("{0}"));

                this.commandBody =
                    NewChain().From(pTk).Select().GetBuilder().Build();

                BindBatchUrl();
                BindBatchBody();
                BindWebRequest();
                ReadResponseStr("POST", _miniFactory.Created());

                if (this.response_ != null && this.response_ != string.Empty)
                {
                    try
                    {
                        ret_ = _jsonmanager.DeserializeFromParentNode<T>(this.response_, "result");
                    }
                    catch (Exception e) { }
                }
            }
            return ret_;
        }
        public IEnumerable<T> SelectFromType<T>(string cond_=null,string dbName_=null) where T : class,IorientDefaultObject
        {
            CheckDbName(dbName_);
            ITypeToken pTk=_typeConverter.Get(typeof(T));
            IEnumerable<T> ret_=new List<T>();
            List<ITypeToken> tt=null;
            ICommandBuilder where_=null;

            if (cond_ != null)
            {
                tt=new List<ITypeToken>() { _miniFactory.NewToken(cond_) };

                where_ =
                    _commandFactory.CommandBuilder(_miniFactory, _formatFactory, tt, _miniFactory.EmptyString()).Build();
            }

            if (pTk != null)
            {
                //_commandFactory.CommandBuilder(_miniFactory, _formatFactory, prsTk, _miniFactory.NewToken("{0}"));  
                

                this.commandBody =
                    NewChain().From(pTk).Select().Where(where_).GetBuilder().Build();

                BindBatchUrl();
                BindBatchBody();
                BindWebRequest();
                ReadResponseStr("POST", _miniFactory.Created());

                if (this.response_ != null && this.response_ != string.Empty)
                {
                    try
                    {
                        ret_ = _jsonmanager.DeserializeFromParentNode<T>(this.response_, "result");
                    }
                    catch (Exception e) { }
                }
            }
            return ret_;
        }
        /// <summary>
        /// Searches single item parsing type to DB.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cond_"></param>
        /// <param name="dbName_"></param>
        /// <returns></returns>
        public T SelectSingle<T>(string cond_,string dbName_=null) 
            where T : class, IorientDefaultObject
        {

            CheckDbName(dbName_);
            ITypeToken pTk = _typeConverter.Get(typeof(T));
            T ret_ = null;
            List<ITypeToken> tt = new List<ITypeToken>() { _miniFactory.NewToken(cond_) };

            if (pTk != null)
            {
                //_commandFactory.CommandBuilder(_miniFactory, _formatFactory, prsTk, _miniFactory.NewToken("{0}"));                
                ICommandBuilder where_ =
                    _commandFactory.CommandBuilder(_miniFactory, _formatFactory, tt, _miniFactory.EmptyString()).Build();

                this.commandBody=
                    NewChain().From(pTk).Select().Where(where_).GetBuilder().Build();

                BindBatchUrl();
                BindBatchBody();
                BindWebRequest();
                ReadResponseStr("POST", _miniFactory.Created());

                if (this.response_ != null && this.response_ != string.Empty)
                {
                    try
                    {
                        ret_ = _jsonmanager.DeserializeFromParentNode<T>(this.response_, "result").FirstOrDefault();
                    }
                    catch (Exception e) { }
                }
            }
            return ret_;
        }
         
        public IEnumerable<T> SelectByGUIDfromType<T> (Type type_,string cond_=null,string dbName_=null)
          where T:class,IorientDefaultObject
        {
          IEnumerable<T> result=null;
          CheckDbName(dbName_);
          List<ITypeToken> tt=null;
          ICommandBuilder where_=null;
          ITypeToken id_ = _typeConverter.Get(type_);

            if (cond_ != null)
            {
                tt=new List<ITypeToken>(){_miniFactory.NewToken(cond_)};

                where_=
                  _commandFactory.CommandBuilder(_miniFactory, _formatFactory, tt, _miniFactory.EmptyString()).Build();
            }

            if (id_.Text!= null)
            {
                //_commandFactory.CommandBuilder(_miniFactory, _formatFactory, prsTk, _miniFactory.NewToken("{0}"));

                this.commandBody =
                  NewChain().From(id_).Select().Where(where_).GetBuilder().Build();

                BindBatchUrl();
                BindBatchBody();
                BindWebRequest();
                ReadResponseStr("POST", _miniFactory.Created());

                if (this.response_ != null && this.response_ != string.Empty)
                {
                    try
                    {
                        result=_jsonmanager.DeserializeFromParentNode<T>(this.response_, "result");
                    }
                    catch (Exception e) { }
                }
            }
          return result;
        }

        public T UpdateEntity<T>(string item_,string dbName_=null)
           where T:class,IorientDefaultObject
        {
          CheckDbName(dbName_);
          T prsTk=OrientStringToObject<T>(item_);
          return UpdateEntity<T>(prsTk,dbName_);
        }
        public T UpdateEntity<T>(T item_,string dbName_=null)
            where T:class,IorientDefaultObject
        {
          CheckDbName(dbName_);
          T ret_ = null;
          if(item_!=null){
            ITypeToken tpTk=_typeConverter.Get(typeof(T));
            ITypeToken prsTk=_miniFactory.NewToken(_jsonmanager.SerializeObject(item_).Replace("\"", "\\\""));
            ITypeToken cnd=_miniFactory.NewToken("GUID='"+item_.GUID+"'");

            ICommandBuilder cb=_commandFactory.CommandBuilder(_miniFactory,_formatFactory,
                new List<ITypeToken>(){cnd},_miniFactory.EmptyString());
            
            if (item_!=null)
            {

                this.commandBody=
                NewChain().Update(tpTk).Content(prsTk)
                .Where(cb)
                .GetBuilder().Build();

                BindBatchUrl();
                BindBatchBody();
                BindWebRequest();
                ReadResponseStr("POST",_miniFactory.Created());

                if (this.response_!=null&&this.response_!=string.Empty)
                {
                    try
                    {
                        ret_=_jsonmanager.DeserializeFromParentNode<T>(this.response_, "result").FirstOrDefault();
                    }
                    catch (Exception e) { }
                }
            }
          }
          return ret_;
        }      

        /// <summary>
        /// Gets values from property collection of From_object, sets to same name properties of To_object. 
        /// Object types the same, no prop type cheking.
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="fromObject"></param>
        /// <param name="toObject"></param>
        /// <returns></returns>
        public T UpdateProperties<T>(T fromObject,T toObject)
          where T:class,IorientDefaultObject
        {       
          T result = null;
          if(fromObject!=null&&toObject!=null){
            result=toObject;

            Type tpFrom=fromObject.GetType();
            Type tpTo=toObject.GetType();

            PropertyInfo[] propertiesFrom=tpFrom.GetProperties();
            PropertyInfo[] propertiesTo=tpTo.GetProperties();

            for(int i=0;i<propertiesFrom.Count();i++)
            {
              bool updateProperty = true;
              int propInd=-1;  
              IEnumerable<Attribute> propAttr = propertiesFrom[i].GetCustomAttributes();
              if( (from s in propAttr where s.GetType().Equals(typeof(Updatable)) select s).Any())
              {

          Updatable atr = (Updatable)
          ((from s in propAttr where s.GetType().Equals(typeof(Updatable)) select s).FirstOrDefault());

          if(atr.isUpdatable==false)
          {
            updateProperty = false;
          }
         
              }

    if(updateProperty==true){
    PropertyInfo proeprtyFrom = propertiesFrom[i];
    var a = proeprtyFrom.GetValue(fromObject, null);

    for(int i2=0;i2<propertiesTo.Count();i2++){
      if(propertiesFrom[i].Name==propertiesTo[i2].Name){
        propInd=i2;
        object val_ = propertiesFrom[i].GetValue(fromObject, null);
        bool toUpdate = true;

        if(val_==null){
toUpdate = false;
        }else{
        if(val_.GetType().Equals(typeof(string))){
        if(val_==string.Empty)
        {
toUpdate = false;
        }
        }
      }

if(toUpdate){
  propertiesTo[i2].SetValue(result, propertiesFrom[i].GetValue(fromObject, null), null);
}

      }
    }

              }
            }
          }

          return result;
        }

        public T PropertyTryReturnAttribute<T>(PropertyInfo p_) 
          where T:System.Attribute
        {
          T item = null;
          try{
          if(p_.GetCustomAttributes().Where(s=>s.GetType().Equals(typeof(T))).Any()){ 
             return (T)p_.GetCustomAttributes().Where(s=>s.GetType().Equals(typeof(T))).FirstOrDefault();
          }
          }catch(Exception e){}
          return null;
        }
       
        //T.inE(InE)
        public IEnumerable<T> Select<T,InE>(T vertexFrom_=null,string dbName_=null)
            where T:class,IOrientVertex where InE:class,IOrientEdge
        {
          CheckDbName(dbName_);
          ITypeToken inVtk=null;
          ITypeToken intETk=null;
          IEnumerable<T> ret_=null;
          ITypeToken alias=_miniFactory.NewToken("a1");

          inVtk =_typeConverter.Get(typeof(T));
          intETk=_typeConverter.Get(typeof(InE));

          if (vertexFrom_ == null)
          {

              this.commandBody =
              NewChain().Select().InE(intETk).As(alias).FromV(inVtk)
              .Expand(alias)
              .GetBuilder().Build();

          }
          else
          {

              ICommandBuilder cb = _commandFactory.CommandBuilder(_miniFactory, _formatFactory,
              new List<ITypeToken>() { _miniFactory.NewToken("@rid=" + vertexFrom_.id) }
              , _miniFactory.EmptyString());

              this.commandBody =
              NewChain().Select().InE(intETk).As(alias).FromV(inVtk)
              .Where(cb)
              .Expand(alias)
              .GetBuilder().Build();

          }

          if (inVtk != null && intETk != null)
          {

              BindBatchUrl();
              BindBatchBody();
              BindWebRequest();
              ReadResponseStr("POST", _miniFactory.Created());

              if (this.response_ != null && this.response_ != string.Empty)
              {
                  try
                  {
                      ret_=_jsonmanager.DeserializeFromParentNode<T>(this.response_, "result");
                  }
                  catch (Exception e) { }
              }
          }
          return ret_;

        }
        //T.OutE(OutE).InV(InV)
        public IEnumerable<InV> Select<T,OutE,InV>(T vertexFrom_=null,string dbName_=null) 
            where T:class,IOrientVertex where OutE:class,IOrientEdge where InV:class,IOrientVertex
        {
          CheckDbName(dbName_);
            ITypeToken outEtk = null;
            ITypeToken inVtk = null;
            ITypeToken fromEtk = null;
            IEnumerable<InV> ret_ = null;
            ITypeToken alias=_miniFactory.NewToken("a1");

            outEtk=_typeConverter.Get(typeof(OutE));
            inVtk=_typeConverter.Get(typeof(InV));
            fromEtk=_typeConverter.Get(typeof(T));

            if (vertexFrom_ == null)
            {

this.commandBody =
NewChain().Select().OutE(outEtk).InV(inVtk).As(alias).FromV(fromEtk)
.Expand(alias)
.GetBuilder().Build();

            }
            else
            {

ICommandBuilder cb=_commandFactory.CommandBuilder(_miniFactory,_formatFactory,
new List<ITypeToken>(){_miniFactory.NewToken("@rid="+vertexFrom_.id)}
,_miniFactory.EmptyString());

this.commandBody =
NewChain().Select().OutE(outEtk).Dot().InV(inVtk).As(alias).FromV(fromEtk)
.Where(cb)
.And().Gap().OutE(outEtk).Dot().InV(null).Dot().ClassCheck(inVtk)
.Expand(alias)
.GetBuilder().Build();

            }

            if (outEtk !=null && inVtk !=null)
            {

                BindBatchUrl();
                BindBatchBody();
                BindWebRequest();
                ReadResponseStr("POST", _miniFactory.Created());

                if (this.response_ != null && this.response_ != string.Empty)
                {
                    try
                    {
                        ret_ = _jsonmanager.DeserializeFromParentNode<InV>(this.response_, "result");
                    }
                    catch (Exception e) { }
                }
            }
            return ret_;
        }
        //T.OutE(OutE).InV(InV)
        public IEnumerable<InV> SelectCommentToComment<T,InE,InV>(T vertexFrom_=null,string dbName_=null)
            where T:class,IOrientVertex where InE:class,IOrientEdge where InV:class,IOrientVertex
        {
          CheckDbName(dbName_);
            ITypeToken inEtk = null;
            ITypeToken inVtk = null;
            ITypeToken fromEtk = null;
            IEnumerable<InV> ret_ = null;
            ITypeToken alias = _miniFactory.NewToken("a1");

            inEtk = _typeConverter.Get(typeof(InE));
            inVtk = _typeConverter.Get(typeof(InV));
            fromEtk = _typeConverter.Get(typeof(T));

            if (vertexFrom_ == null)
            {

                this.commandBody =
                NewChain().Select().InE(inEtk).Dot().InV(inVtk).As(alias).FromV(fromEtk)
                .Expand(alias)
                .GetBuilder().Build();

            }
            else
            {

                ICommandBuilder cb = _commandFactory.CommandBuilder(_miniFactory, _formatFactory,
                new List<ITypeToken>() { _miniFactory.NewToken("@rid=" + vertexFrom_.id) }
                , _miniFactory.EmptyString());

                this.commandBody =
                NewChain().Select().InE(inEtk).Dot().InV(inVtk).As(alias).FromV(fromEtk)
                .Where(cb)
                .Expand(alias)
                .GetBuilder().Build();

            }

            if (inEtk != null && inVtk != null)
            {

                BindBatchUrl();
                BindBatchBody();
                BindWebRequest();
                ReadResponseStr("POST", _miniFactory.Created());

                if (this.response_ != null && this.response_ != string.Empty)
                {
                    try
                    {
                        ret_ = _jsonmanager.DeserializeFromParentNode<InV>(this.response_, "result");
                    }
                    catch (Exception e) { }
                }
            }
            return ret_;
        }
        
        public IEnumerable<T> SelectFromTraverseWithOffset<T,outE,inV,inE,inE2>(string ID_,string depthPropName_,int depthFrom_,int? depthOfset_=null,string dbName_=null)
          where T:class,IorientDefaultObject
        {
          CheckDbName(dbName_);
          IEnumerable<T> ret_=null;
          ITypeToken idTk=_miniFactory.NewToken(ID_);
          ITypeToken tTk=_typeConverter.Get(typeof(T));
          ITypeToken outETk=_typeConverter.Get(typeof(outE));
          ITypeToken inVTk=_typeConverter.Get(typeof(inV));
          ITypeToken inETk=_typeConverter.Get(typeof(inE));
          ITypeToken inETk2=_typeConverter.Get(typeof(inE2));
          ITypeToken _depthPropName=_miniFactory.NewToken(depthPropName_);
          ITypeToken depthFromTk=_miniFactory.NewToken(depthFrom_.ToString());          

          int _depth=depthOfset_==null?depthFrom_+3:depthFrom_+(int)depthOfset_;
          ITypeToken depthToTk=_miniFactory.NewToken(_depth.ToString());

          List<ICommandBuilder> commands=new List<ICommandBuilder>();


          if(tTk!=null&&outETk!=null&&inVTk!=null&&inETk!=null){          

            this.commandBody= 
            //select
            NewChain().Traverse().OutE(outETk).Coma().InV(inVTk).Coma().InE(inETk).Coma().InE(inETk2).FromV(idTk)
            .NestRnd()
            .From().Select()
            .Where(_depthPropName)
            .Between(depthFromTk).And(depthToTk)
            .GetBuilder().Build();         
            
            BindBatchUrl();
            BindBatchBody();
            BindWebRequest();
            ReadResponseStr("POST", _miniFactory.Created());

              if (this.response_ != null && this.response_ != string.Empty)
              {
                  try
                  {
                      ret_ = _jsonmanager.DeserializeFromParentNode<T>(this.response_, "result");
                  }
                  catch (Exception e){}
              }
          }

        return ret_;
        }
        public IEnumerable<T> SelectTraverseWithOffset<T,outE,inV,inE,inE2>(string ID_,string depthPropName_,int depthFrom_,int? depthOfset_=null,string dbName_=null)
          where T:class,IorientDefaultObject
        {
         CheckDbName(dbName_);
          IEnumerable<T> ret_=null;
          ITypeToken idTk=_miniFactory.NewToken(ID_);
          ITypeToken tTk=_typeConverter.Get(typeof(T));
          ITypeToken outETk=_typeConverter.Get(typeof(outE));
          ITypeToken inVTk=_typeConverter.Get(typeof(inV));
          ITypeToken inETk=_typeConverter.Get(typeof(inE));
          ITypeToken inETk2=_typeConverter.Get(typeof(inE2));
          ITypeToken _depthPropName=_miniFactory.NewToken(depthPropName_);
          ITypeToken depthFromTk=_miniFactory.NewToken(depthFrom_.ToString());          

          int _depth=depthOfset_==null?depthFrom_+3:depthFrom_+(int)depthOfset_;
          ITypeToken depthToTk=_miniFactory.NewToken(_depth.ToString());

          List<ICommandBuilder> commands=new List<ICommandBuilder>();


          if(tTk!=null&&outETk!=null&&inVTk!=null&&inETk!=null){          

            this.commandBody= 
            //select
            NewChain().Traverse().OutE(outETk).Coma().InV(inVTk).Coma().InE(inETk).Coma().InE(inETk2).FromV(idTk)                     
            .GetBuilder().Build();         
            
            BindBatchUrl();
            BindBatchBody();
            BindWebRequest();
            ReadResponseStr("POST", _miniFactory.Created());

              if (this.response_ != null && this.response_ != string.Empty)
              {
                  try
                  {
                      ret_ = _jsonmanager.DeserializeFromParentNode<T>(this.response_, "result");
                  }
                  catch (Exception e){}
              }
          }

        return ret_;
        }
        public IEnumerable<T> TraverseFrom<T,outE,inV,inE,inE2>(string ID_,string dbName_=null)
          where T:class,IorientDefaultObject
        {
         CheckDbName(dbName_);
          IEnumerable<T> ret_=null;
          ITypeToken idTk=_miniFactory.NewToken(ID_);
          ITypeToken tTk=_typeConverter.Get(typeof(T));
          ITypeToken outETk=_typeConverter.Get(typeof(outE));
          ITypeToken inVTk=_typeConverter.Get(typeof(inV));
          ITypeToken inETk=_typeConverter.Get(typeof(inE));
          ITypeToken inETk2=_typeConverter.Get(typeof(inE2));
      
          List<ICommandBuilder> commands=new List<ICommandBuilder>();

          if(tTk!=null&&outETk!=null&&inVTk!=null&&inETk!=null){          

            this.commandBody= 
            //select
            NewChain().Traverse().OutE(outETk).Coma().InV(inVTk).Coma().InE(inETk).Coma().InE(inETk2).FromV(idTk)                     
            .GetBuilder().Build();         
            
            BindBatchUrl();
            BindBatchBody();
            BindWebRequest();
            ReadResponseStr("POST", _miniFactory.Created());

              if (this.response_ != null && this.response_ != string.Empty)
              {
                  try
                  {
                      ret_ = _jsonmanager.DeserializeFromParentNode<T>(this.response_, "result");
                  }
                  catch (Exception e){}
              }
          }

        return ret_;
        }
        
        //T.InE().OutV()
        public IEnumerable<OutV> SelectInEOutV<T,InE,OutV>(T vertexFrom_=null,string dbName_=null)
            where T:class,IorientDefaultObject where InE:class,IOrientEdge where OutV:class,IOrientVertex
        {
          CheckDbName(dbName_);
          ITypeToken inEtk = null;
          ITypeToken outVtk = null;
          ITypeToken fromVtk = null;
          IEnumerable<OutV> ret_ = null;
          ITypeToken alias = _miniFactory.NewToken("a1");

          inEtk=_typeConverter.Get(typeof(InE));
          outVtk=_typeConverter.Get(typeof(OutV));
          fromVtk=_typeConverter.Get(typeof(T));

          if (vertexFrom_ == null)
          {
            this.commandBody =
            NewChain().Select().InE(inEtk).Dot().OutV(outVtk).As(alias).FromV(fromVtk)
            .Expand(alias)
            .GetBuilder().Build();
          }
          else
          {
            ICommandBuilder cb = _commandFactory.CommandBuilder(_miniFactory, _formatFactory,
            new List<ITypeToken>() { _miniFactory.NewToken("@rid=" + vertexFrom_.id) }
            , _miniFactory.EmptyString());

            this.commandBody =
            NewChain().Select().InE(inEtk).Dot().OutV(outVtk).As(alias).FromV(fromVtk)
            .Where(cb)
            .Expand(alias)
            .GetBuilder().Build();
          }

          if (inEtk != null && outVtk != null)
          {
            BindBatchUrl();
            BindBatchBody();
            BindWebRequest();
            ReadResponseStr("POST", _miniFactory.Created());

            if (this.response_ != null && this.response_ != string.Empty)
            {
              try
              {
                ret_ = _jsonmanager.DeserializeFromParentNode<OutV>(this.response_, "result");
              }
              catch (Exception e) { }
            }
          }
          return ret_;
        }


        //converting objects,strings and orients
        public T OrientStringToObject<T>(string item_)
            where T:class,IorientDefaultObject
        {
            T result = null;
            result=_jsonmanager.DeserializeFromParentNode<T>(item_,"result").SingleOrDefault();
            return result;
        }
        public T ContentStringToObject<T>(string item_)
            where T:class,IorientDefaultObject
        {
            T result = null;
            result = _jsonmanager.DeserializeFromParentNodeStringObj<T>(item_);
            return result;
        }
        public string ObjectToContentString<T>(T item_)
          where T:class,IorientDefaultObject
        {
            string result = null;
            result = _jsonmanager.SerializeObject(item_);
            return result;
        }
        public string ObjectToContentString<T>(IEnumerable<T> item_)
          where T:class,IorientDefaultObject
        {
            string result = null;
            result = _jsonmanager.SerializeObject(item_);
            return result;
        }
        
        public T TestDeserialize<T>(string item_)
        where T:class
        {
          T result = null;
            result=_jsonmanager.DeserializeFromParentNode<T>(item_,"result").SingleOrDefault();
            return result;
        }

        /// <summary>
        /// Alter property default with functionName. Will be Quoted "f()". Example "uuid()"
        /// </summary>
        /// <param name="class_"></param>
        /// <param name="prop_"></param>
        /// <param name="func_">nameOfFyncion</param>
        public void AlterProperty(ITypeToken class_,ITypeToken prop_,ITypeToken func_)
        {
        
            this.commandBody = this._commandShema.AlterPropertyUUID(class_, prop_, func_)
                .Build();

            BindCommandUrl();
            BindCommandBody();
            BindWebRequest();
            ReadResponseStr("POST", _miniFactory.Created());

            if (this.response_ !=null && this.response_ !=string.Empty)
            {
            
            }
        }

        /// <summary>
        /// Database dateformat.
        /// </summary>
        public void DbPredefinedParameters()
        {
            string dateFormat = ConfigurationManager.AppSettings["OrientDateTime"];
            string cmd="ALTER DATABASE DATETIMEFORMAT \\\""+dateFormat+"\\\"";

            List<ITypeToken> tk = new List<ITypeToken>()
            {
                _miniFactory.NewToken(cmd)
            };

            this.commandBody =
            _commandFactory.CommandBuilder(_miniFactory,_formatFactory,tk,_miniFactory.EmptyString());

            BindBatchUrl();
            BindBatchBody();
            BindWebRequest();
            ReadResponseStr("POST", _miniFactory.Created());

            if (this.response_ != null && this.response_ != string.Empty)
            {
             
            }
        }
        
    }

    public interface IOrientRepo
    {
    void BindDbName(string dbName_);
    void BindUrlName(string input_);
    void AlterProperty(ITypeToken class_, ITypeToken prop_, ITypeToken func_);    
    IOrientRepo CreateClass(string class_, string extends_, string dbName_ = null);
    Type CreateClass<T, K>(string dbName_=null)
      where T : IOrientEntity
      where K : IOrientEntity;
    IOrientRepo CreateDb(string name=null,string host=null);
    T CreateEdge<T>(IOrientEdge edge_, IOrientVertex vFrom, IOrientVertex vTo, string dbName_ = null) where T : class, IOrientEdge; 
    IOrientRepo CreateProperty(string class_, string property_, Type type_, bool mandatory_, bool notnull_, string dbName_ = null);
    T CreateProperty<T>(T item, string dbName_ = null) where T : class, IorientDefaultObject;
    IOrientRepo CreateVertex(string vertex, string content_ = null, string dbName_ = null);
    T CreateVertex<T>(string content_, string dbName_ = null) where T : class, IOrientVertex;
    T CreateVertex<T>(IOrientVertex vertex, string dbName_ = null) where T : class, IOrientVertex;
    void DbPredefinedParameters();
    IOrientRepo Delete<T>(T item = null, string condition_ = null, string dbName_ = null) where T : class, IorientDefaultObject;
    IOrientRepo DeleteDb(string name=null, string host=null);
    IOrientRepo DeleteEdge<T>(string from, string to, string condition_ = null, string dbName_ = null) where T : class, IOrientEdge;
    IOrientRepo DeleteEdge<T, K, C>(K from, C to, string condition_ = null, string dbName_ = null)
      where T : IOrientEdge
      where K : IOrientVertex
      where C : IOrientVertex;
    string GetResult();
    CommandsChain NewChain();
    string ObjectToContentString<T>(T item_) where T : class, IorientDefaultObject;
    T OrientStringToObject<T>(string item_) where T : class, IorientDefaultObject;
    PropertyInfo[] Props<T>() where T :IOrientObject;
    IEnumerable<T> SelectFromType<T>(string cond_ = null, string dbName_ = null) where T : class, IorientDefaultObject;
    IEnumerable<T> Select<T, InE>(T vertexFrom_ = default(T), string dbName_ = null)
      where T : class,IOrientVertex
      where InE :  class,IOrientEdge;
    IEnumerable<InV> Select<T, OutE, InV>(T vertexFrom_ = default(T), string dbName_ = null)
      where T :  class,IOrientVertex
      where OutE :  class,IOrientEdge
      where InV :  class,IOrientVertex;
    IEnumerable<T> SelectAll<T>(IorientDefaultObject t_, string dbName_) where T : class, IorientDefaultObject;
    IEnumerable<InV> SelectCommentToComment<T, InE, InV>(T vertexFrom_ = default(T), string dbName_ = null)
      where T :  class,IOrientVertex
      where InE :  class,IOrientEdge
      where InV :  class,IOrientVertex;

IEnumerable<T> SelectFromTraverseWithOffset<T,outE,inV,inE,inE2>(string ID_,string depthPropName_,int depthFrom_,int? depthOfset_=null,string dbName_=null)
    where T:class,IorientDefaultObject;
IEnumerable<T> SelectTraverseWithOffset<T,outE,inV,inE,inE2>(string ID_,string depthPropName_,int depthFrom_,int? depthOfset_=null,string dbName_=null)
    where T:class,IorientDefaultObject;
IEnumerable<T> TraverseFrom<T,outE,inV,inE,inE2>(string ID_,string dbName_=null)
    where T:class,IorientDefaultObject;

    T SelectSingle<T>(string cond_, string dbName_ = null) where T : class, IorientDefaultObject;
    T ContentStringToObject<T>(string item_) where T : class, IorientDefaultObject;
    T UpdateEntity<T>(T item_, string dbName_ = null) where T : class,IorientDefaultObject;
    T UpdateEntity<T>(string item_, string dbName_ = null) where T : class,IorientDefaultObject;
    }

}
