import axios from "axios";

export const fetcher = (url: string) =>
  axios.get(process.env.NEXT_PUBLIC_BACKEND + url).then((res) => res.data);
