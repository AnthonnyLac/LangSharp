import os
import argparse
from dotenv import load_dotenv
from langchain_community.utilities import SQLDatabase
from langchain_community.agent_toolkits import create_sql_agent
from langchain_openai import ChatOpenAI
from langchain.chat_models import init_chat_model

class SQLChatModel:
    def __init__(self, db_uri: str, api_key: str, model: str, temperature: float = 0, agent_type: str = "openai-tools"):
        self.db = SQLDatabase.from_uri(db_uri)
        self.api_key = api_key
        self.llm = init_chat_model(model, api_key=self.api_key, temperature=temperature)
        self.agent_executor = create_sql_agent(self.llm, db=self.db, agent_type=agent_type, verbose=True)

    def run(self, query):
        return self.agent_executor.invoke(query)
  
def call_llm_sql_langsharp(api_key: str, query: str, model: str, db_uri: str, temperature: float = 0, agent_type="openai-tools"):
    try:
        if not api_key or not api_key.strip():
            raise ValueError("No API key provided")
        
        if not query or not query.strip():
            raise ValueError("No query provided")

        if not model or not model.strip():
            raise ValueError("No model provided")

        if not db_uri or not db_uri.strip():
            raise ValueError("No db_uri provided")

        query_executor = SQLChatModel(db_uri=db_uri, api_key=api_key, model=model, temperature=temperature, agent_type=agent_type)
        return query_executor.run(query)
    except Exception as e:
        return str(e)    