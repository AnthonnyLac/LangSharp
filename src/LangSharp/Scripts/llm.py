import argparse
import openai
from langchain.chat_models import init_chat_model
import os
from dotenv import load_dotenv


class ChatModel:
    def __init__(self, model="gpt-4o-mini", temperature=0.7, api_key=None) -> None:        
        """
        Initialize the ChatModel with the given model, temperature and API key.

        Args:
            model (str, optional): The model to use. Defaults to "gpt-4o-mini".
            temperature (float, optional): The temperature to use. Defaults to 0.7.
            api_key (str, optional): The API key to use. Defaults to None.
        """
        self.model = model
        self.temperature = temperature
        self.api_key = api_key
        self.model = init_chat_model(self.model, api_key=self.api_key, temperature=self.temperature)

    def run(self, prompt: str) -> str:
        """
        Generates a response from the chat model based on the provided prompt.

        Args:
            prompt (str): The input text for which the model generates a response.

        Returns:
            str: The generated content from the chat model.
        """
        response = self.model.invoke(
            input=[
                {"role": "system", "content": "You are a helpful assistant."},
                {"role": "user", "content": prompt},
            ]
        )

        return response.content


def call_llm_langsharp(api_key: str, prompt: str, model: str, temperature=0.7) -> str:
    """
    Calls the LLM LangSharp function with the given arguments.

    Args:
        api_key (str): The API key to use to access the LLM.
        prompt (str): The text prompt to send to the LLM.
        model (str): The model to use to generate the response.
        temperature (float, optional): The temperature to use to generate the response. Defaults to 0.7.

    Returns:
        str: The generated content from the chat model.
    """
    try:
        if not api_key or not api_key.strip():
            raise ValueError("No API key provided")
        
        if not prompt or not prompt.strip():
            raise ValueError("No prompt provided")

        if not model or not model.strip():
            raise ValueError("No model provided")

        openai_client = ChatModel(
            model=model, temperature=temperature, api_key=api_key
        )
        return openai_client.run(prompt)
    except Exception as e:
        return str(e)