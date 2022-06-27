import React, { useState } from "react";
import { Form, Formik } from "formik";
import {
  initialValues,
  validationSchema,
  fieldNames,
  fieldLabels,
} from "./addTodoFormProps";
import { CreateProjectRequestDto } from "../../types";
import {
  Button,
  Card,
  Grid,
  Modal,
  Text,
  Tooltip,
  useTheme,
} from "@nextui-org/react";
import TextField, { TextAreaField } from "../TextField";

export interface AddProjectFormModalProps {
  propsValues?: CreateProjectRequestDto;
  actionButton?: JSX.Element;
  buttonTitle?: string;

  onSubmit: (values: CreateProjectRequestDto) => Promise<void>;
}

const Index = ({
  propsValues,
  actionButton,
  buttonTitle,
  onSubmit,
}: AddProjectFormModalProps) => {
  const { theme } = useTheme();
  const [visible, setVisible] = React.useState(false);

  const handler = () => setVisible(true);

  const closeHandler = () => {
    setVisible(false);
  };
  const [errors, setErrors] = useState<string[] | undefined>();

  const onSubmitHandler = async (values: CreateProjectRequestDto) => {
    try {
      await onSubmit(values);
      setVisible(false);
    } catch (e: any) {
      setErrors(e?.response?.data?.errors);
    }
  };

  const displayErrors = () => (
    <Tooltip content='' contentColor='error'>
      <Grid.Container alignItems='center' gap={1}>
        {errors?.map((e) => (
          <Grid key={e} xs={12}>
            <Text color='error'>{e}</Text>
          </Grid>
        ))}
      </Grid.Container>
    </Tooltip>
  );
  return (
    <Grid.Container
      gap={1}
      css={{ backgroundColor: theme?.colors.backgroundContrast }}
    >
      <Grid xs={12}>
        {actionButton ? (
          <div onClick={handler}>{actionButton}</div>
        ) : (
          <Button auto color='success' shadow onClick={handler}>
            Add new project
          </Button>
        )}
      </Grid>
      <Modal
        closeButton
        blur
        aria-labelledby='modal-title'
        open={visible}
        onClose={closeHandler}
      >
        <Modal.Header>
          <Text id='modal-title' size={18}>
            Add your new{" "}
            <Text b size={18}>
              project
            </Text>
          </Text>
        </Modal.Header>
        <Modal.Body>
          <Formik
            initialValues={propsValues ? propsValues : initialValues}
            onSubmit={onSubmitHandler}
            validationSchema={validationSchema}
          >
            {({ isSubmitting }) => (
              <Form>
                <Card variant='bordered'>
                  <Grid.Container
                    gap={1}
                    justify='center'
                    alignItems='center'
                    css={{
                      backgroundColor: theme?.colors.primaryShadow,
                      border: theme?.borderWeights.light,
                      boxShadow: theme?.shadows.lg,
                      padding: theme?.space.lg,
                    }}
                  >
                    {errors && displayErrors()}
                    <TextField
                      type='text'
                      name={fieldNames.title}
                      label={fieldLabels.title}
                      fullWidth
                    />
                    <TextAreaField
                      type='password'
                      name={fieldNames.description}
                      label={fieldLabels.description}
                      placeholder='Enter your amazing ideas.'
                      fullWidth
                    />

                    <Grid xs={12}>
                      <Button
                        type='submit'
                        color='success'
                        disabled={isSubmitting}
                      >
                        {buttonTitle ? buttonTitle : "Create"}
                      </Button>
                    </Grid>
                  </Grid.Container>
                </Card>
              </Form>
            )}
          </Formik>
        </Modal.Body>
      </Modal>
    </Grid.Container>
  );
};

export default Index;
