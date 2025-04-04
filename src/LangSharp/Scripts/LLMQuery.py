import os
import argparse
from dotenv import load_dotenv
from langchain_community.utilities import SQLDatabase
from langchain_community.agent_toolkits import create_sql_agent
from langchain_openai import ChatOpenAI

class QueryExecutor:
    def __init__(self, db_uri, api_key, model, temperature, agent_type):
        self.db = SQLDatabase.from_uri(db_uri)
        self.api_key = api_key
        self.llm = ChatOpenAI(model=model, temperature=temperature, api_key=api_key)
        self.agent_executor = create_sql_agent(self.llm, db=self.db, agent_type=agent_type, verbose=True)

    def run(self, query):
        return self.agent_executor.invoke(query)
  
def CallOpenIAQueryLangSharp(api_key, query, model, db_uri, temperature=0, agent_type="openai-tools"):
    try:
        if not api_key or not api_key.strip():
            raise ValueError("No API key provided")
        
        if not query or not query.strip():
            raise ValueError("No query provided")

        if not model or not model.strip():
            raise ValueError("No model provided")

        if not db_uri or not db_uri.strip():
            raise ValueError("No db_uri provided")

        query_executor = QueryExecutor(db_uri=db_uri, api_key=api_key, model=model, temperature=temperature, agent_type=agent_type)
        return query_executor.run(query)
    except Exception as e:
        return str(e)


def main(args):
    load_dotenv('.env', override=True)

    api_key = (
        args.api_key
        or os.getenv("OPENAI_API_KEY")
    )
    if not api_key:
        raise ValueError("No API key provided")

    query_executor = QueryExecutor(args.db_uri, api_key=api_key)
    result = query_executor.run(args.query)
    print(result)


if __name__ == "__main__":
    parser = argparse.ArgumentParser()
    parser.add_argument("-q", "--query", type=str, help="The query to run", required=True)
    parser.add_argument("-d", "--db_uri", type=str, help="The URI of the database to connect to", required=True)
    parser.add_argument("-k", "--api_key", type=str, help="The OpenAI API key")
    parser.add_argument("-kp", "--api_key_path", type=str, help="The path to the file containing the OpenAI API key")
    parser.add_argument("-m", "--model", type=str, help="The OpenAI model to use", default="gpt-4o-mini")
    parser.add_argument("-t", "--temperature", type=float, help="The temperature to use for the OpenAI model", default=0)

    main(parser.parse_args())
    