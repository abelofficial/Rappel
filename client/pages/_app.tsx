import { NextUIProvider } from "@nextui-org/react";
import { AuthProvider } from "../Contexts/Auth";

function MyApp({ Component, pageProps, fallback }: any) {
  return (
    // 2. Use at the root of your app
    <NextUIProvider>
      <AuthProvider>
        <Component {...pageProps} />
      </AuthProvider>
    </NextUIProvider>
  );
}

export default MyApp;
