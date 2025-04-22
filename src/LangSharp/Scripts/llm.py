import argparse
import openai
from langchain.chat_models import init_chat_model
import os
from dotenv import load_dotenv


class ChatOpenAI:
    def __init__(self, model="gpt-4o-mini", temperature=0.7, api_key=None):
        self.model = model
        self.temperature = temperature
        self.api_key = api_key
        self.model = init_chat_model(self.model, api_key=self.api_key, temperature=self.temperature)

    def call_openai_api(self, prompt):
        response = self.model.invoke(
            input=[
                {"role": "system", "content": "You are a helpful assistant."},
                {"role": "user", "content": prompt},
            ]
        )

        return response.content

def CallOpenIALangSharp(api_key, prompt, model, temperature=0.7):
    try:
        if not api_key or not api_key.strip():
            raise ValueError("No API key provided")
        
        if not prompt or not prompt.strip():
            raise ValueError("No prompt provided")

        if not model or not model.strip():
            raise ValueError("No model provided")

        openai_client = ChatOpenAI(
            model=model, temperature=temperature, api_key=api_key
        )
        return openai_client.call_openai_api(prompt)
    except Exception as e:
        return str(e)


def main(args):
    load_dotenv(override=True)

    print(args)

    api_key = (
        args.api_key
        or os.getenv("OPENAI_API_KEY")
    )
    if not api_key:
        raise ValueError("No API key provided")

    openai_client = ChatOpenAI(
        model=args.model, temperature=args.temperature, api_key=api_key
    )
    print(openai_client.call_openai_api(args.prompt))


if __name__ == "__main__":
    parser = argparse.ArgumentParser()

    parser.add_argument("-p", "--prompt", type=str, help="The query to run", required=True)
    parser.add_argument("-k", "--api_key", type=str, help="The OpenAI API key")
    parser.add_argument("-kp", "--api_key_path", type=str, help="The path to the file containing the OpenAI API key")
    parser.add_argument("-m", "--model", type=str, help="The OpenAI model to use", default="gpt-4o-mini")
    parser.add_argument("-t", "--temperature", type=float, help="The temperature to use for the OpenAI model", default=0.7)

    main(parser.parse_args())


    

