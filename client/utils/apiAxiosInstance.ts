import axios from "axios";

export const api = (token?: string) =>
  axios.create({
    baseURL: process.env.NEXT_PUBLIC_BACKEND,
    headers: {
      Authorization: `bearer ${token?.slice(1, token.length - 1)}`,
      "Access-Control-Allow-Origin": "*",
      "Access-Control-Allow-Methods": "GET,PUT,POST,DELETE,PATCH,OPTIONS",
    },
  });
