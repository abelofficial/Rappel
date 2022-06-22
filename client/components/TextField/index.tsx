import { Grid, Input, Text } from "@nextui-org/react";
import { useField } from "formik";
import React from "react";

export interface TextFieldProps {
  type: string;
  name: string;
  label: string;
  fullWidth: boolean;
}

const Index = ({ label, ...restProps }: TextFieldProps) => {
  const [field, meta, helpers] = useField(restProps);
  return (
    <Grid
      xs={12}
      css={{
        display: "flex",
        flexDirection: "column",
      }}
    >
      <label>
        {label}
        <Input
          {...field}
          {...restProps}
          status={meta.touched && meta.error ? "error" : "default"}
        />
      </label>
      {meta.touched && meta.error ? (
        <Text color='error'>{meta.error}</Text>
      ) : null}
    </Grid>
  );
};

export default Index;
