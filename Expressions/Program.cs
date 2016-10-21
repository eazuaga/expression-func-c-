﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Expressions
{
    class Program
    {
        static void Main(string[] args)
        {
            BlockExpression blockExpr = Expression.Block(Expression.Call(null,typeof(Console).GetMethod("Write", new Type[] { typeof(String) }),  Expression.Constant("Hello ")
       ),
            Expression.Call(
                null,
                typeof(Console).GetMethod("WriteLine", new Type[] { typeof(String) }),
                Expression.Constant("World!")
                ),
            Expression.Constant(42)
        );

            Console.WriteLine("The result of executing the expression tree:");
            // The following statement first creates an expression tree,
            // then compiles it, and then executes it.           
            var result = Expression.Lambda<Func<int>>(blockExpr).Compile()();

            // Print out the expressions from the block expression.
            Console.WriteLine("The expressions from the block expression:");
            foreach (var expr in blockExpr.Expressions)
                Console.WriteLine(expr.ToString());

            // Print out the result of the tree execution.
            Console.WriteLine("The return value of the block expression:");
            Console.WriteLine(result);



            // Lambda expression as executable code.
            Func<int, bool> deleg = i => i < 5;
            // Invoke the delegate and display the output.
            Console.WriteLine("deleg(4) = {0}", deleg(4));

            // Lambda expression as data in the form of an expression tree.
            //Func<int, bool> expr = i => i < 5;
            // Compile the expression tree into executable code.
           // Func<int, bool> deleg2 = expr.Compile();
            // Invoke the method and print the output.
           // Console.WriteLine("deleg2(4) = {0}", deleg2(4));

            /*  This code produces the following output:

                deleg(4) = True
                deleg2(4) = True
            */
           var t = test.rule(i => i < 5);

           //Expression<Func<string, int>> length = s => s.Length;
           Func<string, int> length = s => s.Length;

           string myString = "some string";
           int stringLenght = length(myString);

            var rule = new Rule<User>();
            rule.BindRuleTo(u => u.Username);
        }
    }

    class test{
       static public bool rule (Func<int,bool> predicate){

           var pre = predicate;
           return true;

       }
    
    }

    public class Rule<T>
    {
        public Rule<T> BindRuleTo<U>(Expression<Func<T, U>> expression)
        {
            var lambda = (LambdaExpression)expression;

            if (lambda.Body.NodeType != ExpressionType.MemberAccess)
            {
                throw new InvalidOperationException("Expression must be a MemberExpression");
            }

            var memberExpression = (MemberExpression)lambda.Body;


            return this;
        }
        //public Rule<T> BindRuleTo2<U>(Expression<Func<T, U>> expression)
        //{
        //    this.PropertyName = ExpressionUtil.GetPropertyNameFromExpression(expression);
        //    return this;
        //}
    }


    /*
     [Validator(typeof(PersonValidator))]
public class Person {
	public int Id { get; set; }
	public string Name { get; set; }
	public string Email { get; set; }
	public int Age { get; set; }
}
 
public class PersonValidator : AbstractValidator<Person> {
	public PersonValidator() {
		RuleFor(x => x.Id).NotNull();
		RuleFor(x => x.Name).Length(0, 10);
		RuleFor(x => x.Email).EmailAddress();
		RuleFor(x => x.Age).InclusiveBetween(18, 60);
	}
}
     
     */
}