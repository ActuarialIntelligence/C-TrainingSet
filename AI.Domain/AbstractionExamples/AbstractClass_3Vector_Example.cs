using System;

namespace AI.Domain.AbstractionExamples
{
    /// <summary>
    /// Abstract classes are similar to interfaces in a way.
    /// The difference is that one can have both method signatures that 
    /// we wish for the inheriting class to implement, along with 
    /// concretely implemented methods.
    /// There are additional options here, one can have concrete methods
    /// that are 'overridable' by the inheriting class with the virtual
    /// keyword to re-implement.
    /// These are to be carefuly crafted for the purpose of establishing behaviour 
    /// of the inheriting class
    /// </summary>
    internal abstract class AbstractSpotYield
    {
        internal abstract Term? Term { get; set; }
        internal abstract decimal? Yield { get; set; }


        /// <summary>
        /// We want the inheriting class to have a ToXML method
        /// that IT must implement.
        /// </summary>
        internal abstract string ToXML();

        /// <summary>
        /// This method marked virtual can be re-implemented within the
        /// inheriting class by overriding, or be used as is.
        /// </summary>
        /// <param name="Term"></param>
        internal virtual void ChangeTerm(Term Term)
        {
            this.Term = Term;
        }
    
    }


    /// <summary>
    /// done for teaching only and is bad practice to put
    /// multiple classes within the same file.
    /// 
    /// This is the class that one can use as implemented and concrete.
    /// This class can be newed up with the new keyword. 
    /// </summary>
    /// 
    internal class SpotYield : AbstractSpotYield
    {
        internal override Term? Term 
        {
            get
            { return this.Term; }
            set 
            { this.Term = Term; } 
        
        }
        internal override decimal? Yield
        {    get
            { return this.Yield; }
            set 
            { this.Yield = Yield; }

    }
    internal override string ToXML()
        {
            throw new NotImplementedException(); 
            // This still needs implementation as a student task
        }

        // override the ChangeTerm Method to include functionality.
    }


    public enum Term
    {
        MonthlyEffective,
        YearlyEffective
    }
}
