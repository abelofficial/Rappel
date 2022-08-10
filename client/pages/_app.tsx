import { NextUIProvider } from "@nextui-org/react";
import Layout from "../components/Layout";
import { AuthProvider } from "../Contexts/Auth";
import theme from "../utils/Theme";
import { SSRProvider } from "@react-aria/ssr";

function MyApp({ Component, pageProps, fallback }: any) {
  return (
    // 2. Use at the root of your app
    <NextUIProvider theme={theme}>
      <SSRProvider>
        <AuthProvider>
          <Layout>
            <Component {...pageProps} />
          </Layout>
        </AuthProvider>
      </SSRProvider>
    </NextUIProvider>
  );
}

export default MyApp;
