import axios from "axios";

export const api = (token?: string) =>
  axios.create({
    baseURL: process.env.NEXT_PUBLIC_BACKEND,
    headers: {
      Authorization: `bearer ${token}`,
      "Access-Control-Allow-Origin": "*",
      "Access-Control-Allow-Methods": "GET,PUT,POST,DELETE,PATCH,OPTIONS",
      "Content-Type": "application/json",
      Accept: "application/json",
    },
  });
